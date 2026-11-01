#!/usr/bin/env node
/**
 * Locale tooling for web/src/locales.
 *
 * The web UI keeps its own strings (ACCOUNT_*, BATTLE_*, WF_*, ...) in
 * web/src/locales/<lang>.ts. Game strings ([SomeKey]) come from the API and are
 * not stored here. enUS is the reference: every other language should have the
 * same key set.
 *
 * Usage:
 *   node scripts/locale-tools.mjs check [--json]
 *       Coverage table for every language + keys used in src but missing from enUS.
 *       Also cross-checks game keys against the master data when it can find it
 *       (../MementoMoriData/Master, or $MEMENTOMORI_MASTER).
 *
 *   node scripts/locale-tools.mjs missing <lang> [--snippet]
 *       List keys missing in <lang> with the English source text.
 *       --snippet prints a paste-ready TS object block.
 *
 *   node scripts/locale-tools.mjs extract <lang> [out.json]
 *       Write { "KEY": "english text" } for every missing key (defaults to
 *       .locale-pending/<lang>.json).
 *
 *   node scripts/locale-tools.mjs apply <lang> <file.json>
 *       Merge a { "KEY": "translation" } file into web/src/locales/<lang>.ts.
 *       Only keys the file already knows are accepted; existing values are kept
 *       unless --force is given. Placeholders are validated first.
 *
 *   node scripts/locale-tools.mjs used
 *       Keys referenced by t('...') / translate('...') in web/src that are not in
 *       enUS (game keys in [brackets] are excluded, they come from the API).
 */
import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const ROOT = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const LOCALE_DIR = path.join(ROOT, 'web/src/locales');
const SRC_DIR = path.join(ROOT, 'web/src');
const BASE_LANG = 'enUS';
const PENDING_DIR = path.join(ROOT, '.locale-pending');

const argv = process.argv.slice(2);
const command = argv[0];
const flags = new Set(argv.filter(a => a.startsWith('--')));
const args = argv.filter(a => !a.startsWith('--'));

/** Read a locale file into a plain object (order preserved). */
function loadLocale(lang) {
    const file = path.join(LOCALE_DIR, `${lang}.ts`);
    if (!fs.existsSync(file)) fail(`no such locale: ${file}`);
    const source = fs.readFileSync(file, 'utf8').replace(/^export default/m, 'module.exports =');
    const mod = { exports: {} };
    new Function('module', `${source}\n;return module.exports;`)(mod);
    return mod.exports;
}

function locales() {
    return fs
        .readdirSync(LOCALE_DIR)
        .filter(f => f.endsWith('.ts') && f !== 'index.ts')
        .map(f => f.replace(/\.ts$/, ''));
}

function fail(message) {
    console.error(`error: ${message}`);
    process.exit(1);
}

/** {0}, {0:D2}, {1:N0} ... */
function placeholders(text) {
    return [...String(text).matchAll(/\{\d+(?::[^}]+)?\}/g)].map(m => m[0]).sort();
}

function samePlaceholders(a, b) {
    const pa = placeholders(a);
    const pb = placeholders(b);
    return pa.length === pb.length && pa.every((v, i) => v === pb[i]);
}

/** Keys referenced by t('...') / translate('...') anywhere in web/src. */
function usedKeys() {
    const pattern = /\bt\(\s*['"]([^'"]+)['"]/g;
    const translatePattern = /\btranslate\(\s*['"]([^'"]+)['"]/g;
    const found = new Set();
    const walk = dir => {
        for (const entry of fs.readdirSync(dir, { withFileTypes: true })) {
            const full = path.join(dir, entry.name);
            if (entry.isDirectory()) walk(full);
            else if (/\.(ts|tsx)$/.test(entry.name)) {
                const source = fs.readFileSync(full, 'utf8');
                for (const re of [pattern, translatePattern]) {
                    re.lastIndex = 0;
                    let match;
                    while ((match = re.exec(source))) found.add(match[1]);
                }
            }
        }
    };
    walk(SRC_DIR);
    return found;
}

/** Optional master data, used to check the game-provided keys ([Foo]). */
function masterDir() {
    const candidates = [
        process.env.MEMENTOMORI_MASTER,
        path.join(ROOT, '../MementoMoriData/Master'),
        path.join(ROOT, 'Master'),
    ].filter(Boolean);
    return candidates.find(dir => fs.existsSync(path.join(dir, 'TextResourceEnUsMB.json'))) ?? null;
}

const MASTER_LANG = {
    jaJP: 'JaJp', enUS: 'EnUs', koKR: 'KoKr', zhTW: 'ZhTw', zhCN: 'ZhCn',
    frFR: 'FrFr', deDE: 'DeDe', esMX: 'EsMx', ptBR: 'PtBr', thTH: 'ThTh',
    idID: 'IdId', viVN: 'ViVn', ruRU: 'RuRu', arEg: 'ArEg',
};

function loadMaster(lang) {
    const dir = masterDir();
    const suffix = MASTER_LANG[lang];
    if (!dir || !suffix) return null;
    const file = path.join(dir, `TextResource${suffix}MB.json`);
    if (!fs.existsSync(file)) return null;
    const rows = JSON.parse(fs.readFileSync(file, 'utf8'));
    const map = new Map();
    for (const row of rows) map.set(row.StringKey, row.Text ?? '');
    return map;
}

function coverage() {
    const base = loadLocale(BASE_LANG);
    const baseKeys = Object.keys(base);
    const used = usedKeys();
    const globalMissing = [...used].filter(k => !k.startsWith('[') && !(k in base));

    return locales().map(lang => {
        const table = loadLocale(lang);
        const missing = baseKeys.filter(k => !(k in table));
        const empty = baseKeys.filter(k => k in table && !String(table[k] ?? '').trim());
        const broken = baseKeys.filter(k => k in table && !samePlaceholders(base[k], table[k]));
        return { lang, keys: Object.keys(table).length, missing, empty, broken, table };
    }).concat().map(row => ({
        ...row,
        usedKeysMissing: lang_is_base(row.lang) ? [] : globalMissing,
        gameKeys: row.lang === BASE_LANG ? null : gameCoverage(row.lang, used),
    }));
}

function lang_is_base(lang) {
    return lang === BASE_LANG;
}

function gameCoverage(lang, used) {
    const master = loadMaster(lang);
    if (!master) return null;
    const gameKeys = [...used].filter(k => k.startsWith('['));
    const enMaster = loadMaster(BASE_LANG);
    let missing = 0;
    let empty = 0;
    let fallback = 0;
    for (const key of gameKeys) {
        const value = master.get(key);
        if (value === undefined) {
            missing++;
            continue;
        }
        if (!value.trim()) {
            empty++;
            if (enMaster?.get(key)?.trim()) fallback++;
        }
    }
    return { total: gameKeys.length, missing, empty, fallback };
}

function pad(value, width) {
    return String(value).padEnd(width);
}

function cmdCheck() {
    const rows = coverage();
    if (flags.has('--json')) {
        console.log(JSON.stringify(rows.map(({ table, ...rest }) => rest), null, 2));
        return;
    }
    console.log(`reference: ${BASE_LANG}   locales: web/src/locales   game data: ${masterDir() ?? '(not found)'}\n`);
    console.log(`${pad('lang', 8)}${pad('keys', 7)}${pad('missing', 9)}${pad('empty', 7)}${pad('bad {}', 8)}game keys (missing/empty)`);
    for (const row of rows) {
        const game = row.gameKeys
            ? `${row.gameKeys.total} used, ${row.gameKeys.missing} missing, ${row.gameKeys.empty} empty`
            : '-';
        console.log(`${pad(row.lang, 8)}${pad(row.keys, 7)}${pad(row.missing.length, 9)}${pad(row.empty.length, 7)}${pad(row.broken.length, 8)}${game}`);
    }
    const gaps = rows.filter(r => r.usedKeysMissing.length);
    if (gaps.length) {
        console.log(`\nused in src but missing from ${BASE_LANG} (${gaps[0].usedKeysMissing.length}):`);
        console.log(gaps[0].usedKeysMissing.join(', '));
    }
    const problems = rows.filter(r => r.broken.length);
    for (const row of problems) {
        console.log(`\nplaceholder mismatch in ${row.lang}: ${row.broken.join(', ')}`);
    }
}

function cmdMissing(lang) {
    const base = loadLocale(BASE_LANG);
    const table = loadLocale(lang);
    const missing = Object.keys(base).filter(k => !(k in table));
    if (flags.has('--snippet')) {
        console.log(`export default {\n${missing.map(k => `    '${k}': ${JSON.stringify(base[k])},`).join('\n')}\n};`);
        return;
    }
    console.log(`${lang}: ${missing.length} key(s) missing\n`);
    for (const key of missing) console.log(`${pad(key, 40)} ${base[key]}`);
}

function cmdExtract(lang, outFile) {
    const base = loadLocale(BASE_LANG);
    const table = loadLocale(lang);
    const missing = Object.keys(base).filter(k => !(k in table));
    const out = {};
    for (const key of missing) out[key] = base[key];
    const target = outFile ?? path.join(PENDING_DIR, `${lang}.json`);
    fs.mkdirSync(path.dirname(target), { recursive: true });
    fs.writeFileSync(target, `${JSON.stringify(out, null, 2)}\n`, 'utf8');
    console.log(`${missing.length} key(s) -> ${path.relative(ROOT, target)}`);
}

function cmdApply(lang, file) {
    if (!file) fail('usage: apply <lang> <file.json>');
    const base = loadLocale(BASE_LANG);
    const table = loadLocale(lang);
    const incoming = JSON.parse(fs.readFileSync(file, 'utf8'));
    const force = flags.has('--force');

    // enUS 是基准, 它可以新增 key; 其他语言只能补 enUS 里已有的, 防止拼错
    const unknown = lang === BASE_LANG ? [] : Object.keys(incoming).filter(k => !(k in base));
    if (unknown.length) fail(`${unknown.length} key(s) not in ${BASE_LANG}: ${unknown.slice(0, 5).join(', ')}`);

    const problems = [];
    for (const [key, value] of Object.entries(incoming)) {
        if (!String(value ?? '').trim()) problems.push(`${key}: empty`);
        else if (!samePlaceholders(base[key], value)) problems.push(`${key}: placeholders {${placeholders(base[key]).join(' ')}} != {${placeholders(value).join(' ')}}`);
    }
    if (problems.length) {
        console.error(`refusing to write ${lang}, ${problems.length} problem(s):`);
        for (const p of problems.slice(0, 20)) console.error(`  ${p}`);
        process.exit(1);
    }

    const additions = Object.keys(incoming)
        .filter(k => force || !(k in table))
        .sort((a, b) => a.localeCompare(b));

    if (!additions.length) {
        console.log(`${lang}: nothing to do`);
        return;
    }

    const filePath = path.join(LOCALE_DIR, `${lang}.ts`);
    const source = fs.readFileSync(filePath, 'utf8');
    const closing = source.lastIndexOf('};');
    if (closing < 0) fail('cannot find the closing "};" in ' + filePath);

    const block = additions
        .map(key => `    ${JSON.stringify(key)}: ${JSON.stringify(incoming[key])},`)
        .join('\n');
    const next = `${source.slice(0, closing).replace(/\s*$/, '')}\n\n    // Added by scripts/locale-tools.mjs\n${block}\n${source.slice(closing)}`;
    fs.writeFileSync(filePath, next, 'utf8');
    console.log(`${lang}: +${additions.length} key(s)`);
}

function cmdUsed() {
    const base = loadLocale(BASE_LANG);
    const used = [...usedKeys()].filter(k => !k.startsWith('['));
    const missing = used.filter(k => !(k in base));
    console.log(`${used.length} app key(s) used, ${missing.length} not in ${BASE_LANG}`);
    for (const key of missing) console.log(key);
}

const commands = {
    check: cmdCheck,
    missing: () => cmdMissing(args[1]),
    extract: () => cmdExtract(args[1], args[2]),
    apply: () => cmdApply(args[1], args[2]),
    used: cmdUsed,
};

if (!commands[command]) {
    console.error(fs.readFileSync(fileURLToPath(import.meta.url), 'utf8').split('*/')[0].replace(/^\s*\/\*\*?/m, ''));
    process.exit(1);
}
if (['missing', 'extract', 'apply'].includes(command) && !args[1]) fail(`usage: ${command} <lang>`);
commands[command]();
