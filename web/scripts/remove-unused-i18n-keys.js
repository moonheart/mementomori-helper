/**
 * 查找并删除未使用的国际化 key
 *
 * 使用方法: node scripts/remove-unused-i18n-keys.js [--dry-run]
 * --dry-run: 只显示未使用的 key，不实际删除
 */

import fs from 'fs';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

// 配置
const LOCALES_DIR = path.join(__dirname, '../src/locales');
const SRC_DIR = path.join(__dirname, '../src');
const LOCALE_FILES = ['zhCN.ts', 'zhTW.ts', 'enUS.ts', 'jaJP.ts', 'koKR.ts'];
const BASE_LOCALE = 'zhCN.ts'; // 以 zhCN.ts 为基准提取 keys

// 解析命令行参数
const isDryRun = process.argv.includes('--dry-run');

/**
 * 从 locale 文件中提取所有的 key
 */
function extractKeysFromFile(filePath) {
    const content = fs.readFileSync(filePath, 'utf-8');
    const keys = [];

    // 匹配 'KEY_NAME': 形式的 key
    const regex = /['"]([A-Z_][A-Z0-9_]*)['"]\s*:/g;
    let match;

    while ((match = regex.exec(content)) !== null) {
        keys.push(match[1]);
    }

    return keys;
}

/**
 * 递归获取目录下所有文件
 */
function getAllFiles(dir, extensions = ['.ts', '.tsx', '.js', '.jsx']) {
    const files = [];

    function traverse(currentDir) {
        const items = fs.readdirSync(currentDir);

        for (const item of items) {
            const fullPath = path.join(currentDir, item);
            const stat = fs.statSync(fullPath);

            if (stat.isDirectory()) {
                traverse(fullPath);
            } else if (stat.isFile()) {
                const ext = path.extname(fullPath);
                if (extensions.includes(ext)) {
                    files.push(fullPath);
                }
            }
        }
    }

    traverse(dir);
    return files;
}

/**
 * 在代码中搜索 key 的使用
 */
function findKeyUsageInCode(keys, srcDir) {
    const usedKeys = new Set();
    const files = getAllFiles(srcDir);

    // 排除 locales 目录
    const localeFilesSet = new Set(LOCALE_FILES.map(f => path.join(LOCALES_DIR, f)));
    const filteredFiles = files.filter(f => !localeFilesSet.has(f));

    console.log(`正在搜索 ${filteredFiles.length} 个文件...`);

    for (const file of filteredFiles) {
        const content = fs.readFileSync(file, 'utf-8');

        for (const key of keys) {
            // 检查各种使用模式:
            // 1. t('KEY') - 函数调用
            // 2. 'KEY' 或 "KEY" 作为字符串值（如映射表中）
            // 3. 模板字符串中的 key
            const escapedKey = key.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

            // 匹配 key 作为字符串出现的各种情况
            const patterns = [
                // t('KEY') 或 t("KEY") 或 t(`KEY`)
                new RegExp(`\\bt\\(['"\`]${escapedKey}['"\`]\\)`, 'g'),
                // 作为字符串值: 'KEY' 或 "KEY"
                new RegExp(`['"]${escapedKey}['"]`, 'g'),
            ];

            for (const pattern of patterns) {
                if (pattern.test(content)) {
                    usedKeys.add(key);
                    break;
                }
            }
        }
    }

    return usedKeys;
}

/**
 * 从 locale 文件中删除未使用的 key
 */
function removeUnusedKeysFromFile(filePath, unusedKeys) {
    let content = fs.readFileSync(filePath, 'utf-8');
    const lines = content.split('\n');
    const newLines = [];
    let removedCount = 0;

    for (const line of lines) {
        // 检查这一行是否包含要删除的 key
        let shouldRemove = false;

        for (const key of unusedKeys) {
            const escapedKey = key.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
            const keyPattern = new RegExp(`^\\s*['"\`]${escapedKey}['"\`]\\s*:`);
            if (keyPattern.test(line)) {
                shouldRemove = true;
                removedCount++;
                break;
            }
        }

        if (!shouldRemove) {
            newLines.push(line);
        }
    }

    if (!isDryRun && removedCount > 0) {
        fs.writeFileSync(filePath, newLines.join('\n'), 'utf-8');
    }

    return removedCount;
}

/**
 * 主函数
 */
function main() {
    console.log('=== 查找未使用的国际化 key ===\n');
    console.log(`模式: ${isDryRun ? '预览模式 (不实际删除)' : '执行删除模式'}\n`);

    // 1. 从基准文件提取所有 key
    const baseFilePath = path.join(LOCALES_DIR, BASE_LOCALE);
    const allKeys = extractKeysFromFile(baseFilePath);
    console.log(`从 ${BASE_LOCALE} 提取到 ${allKeys.length} 个 key\n`);

    // 2. 在代码中搜索使用情况
    const usedKeys = findKeyUsageInCode(allKeys, SRC_DIR);
    console.log(`发现 ${usedKeys.size} 个 key 被使用\n`);

    // 3. 找出未使用的 key
    const unusedKeys = allKeys.filter(key => !usedKeys.has(key));
    console.log(`发现 ${unusedKeys.length} 个未使用的 key:\n`);

    if (unusedKeys.length > 0) {
        console.log('未使用的 key 列表:');
        console.log('─'.repeat(50));
        unusedKeys.forEach((key, index) => {
            console.log(`${(index + 1).toString().padStart(4)}. ${key}`);
        });
        console.log('─'.repeat(50));
        console.log(`总计: ${unusedKeys.length} 个未使用的 key\n`);
    }

    if (unusedKeys.length === 0) {
        console.log('没有发现未使用的 key，所有 key 都在使用中！');
        return;
    }

    // 4. 从所有语言文件中删除未使用的 key
    console.log('\n处理各语言文件:');
    console.log('─'.repeat(50));

    for (const localeFile of LOCALE_FILES) {
        const filePath = path.join(LOCALES_DIR, localeFile);

        if (!fs.existsSync(filePath)) {
            console.log(`${localeFile}: 文件不存在，跳过`);
            continue;
        }

        const removedCount = removeUnusedKeysFromFile(filePath, unusedKeys);
        console.log(`${localeFile}: ${isDryRun ? '将删除' : '已删除'} ${removedCount} 个 key`);
    }

    console.log('─'.repeat(50));
    console.log(`\n${isDryRun ? '预览完成，使用不带 --dry-run 参数执行实际删除' : '删除完成！'}`);
}

// 运行
main();
