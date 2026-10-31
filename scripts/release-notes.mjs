import fs from 'node:fs';

const version = (process.env.RELEASE_VERSION || '').replace(/^v/, '');
const sha = process.env.GITHUB_SHA || '';
const repo = process.env.GITHUB_REPOSITORY || '';

const escapeRegex = (value) => value.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

if (!fs.existsSync('CHANGELOG.md')) {
  const fallback = version
    ? `Release ${version}`
    : `Automated dev build from master.${sha ? `\n\nCommit: \`${sha.slice(0, 7)}\`` : ''}`;
  fs.writeFileSync('release-notes.md', `${fallback}\n`);
  console.log(`CHANGELOG.md not found; wrote fallback for ${version || 'dev'}`);
  process.exit(0);
}

const lines = fs.readFileSync('CHANGELOG.md', 'utf8').split(/\r?\n/);

let start = -1;
if (version) {
  const re = new RegExp(`^##\\s+\\[?v?${escapeRegex(version)}\\]?(\\s|$)`);
  start = lines.findIndex((line) => re.test(line));
} else {
  start = lines.findIndex((line) => /^##\s+\[?Unreleased\]?(\s|$)/i.test(line));
}

let body = '';
if (start !== -1) {
  let end = lines.findIndex((line, index) => index > start && /^##\s+/.test(line));
  if (end === -1) end = lines.length;
  body = lines.slice(start + 1, end).join('\n').trim();
}

if (!body) {
  body = version
    ? `Release ${version}`
    : `Automated dev build from master.${sha ? `\n\nCommit: \`${sha.slice(0, 7)}\`` : ''}`;
}

if (repo) {
  const link = version
    ? `https://github.com/${repo}/blob/master/CHANGELOG.md`
    : sha
      ? `https://github.com/${repo}/commit/${sha}`
      : `https://github.com/${repo}`;
  body += `\n\n**Full Changelog**: ${link}`;
}

fs.writeFileSync('release-notes.md', `${body}\n`);
console.log(`Wrote release-notes.md for ${version || 'dev'}`);
