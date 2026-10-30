/**
 * 搜索代码中硬编码的中文字符串（排除注释）
 *
 * 使用方法: node scripts/find-hardcoded-chinese.js [--fix]
 * --fix: 只显示结果，不执行修复
 */

import fs from 'fs';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

// 配置
const SRC_DIR = path.join(__dirname, '../src');
const EXTENSIONS = ['.ts', '.tsx', '.js', '.jsx'];
// 排除的目录和文件
const EXCLUDE_DIRS = ['node_modules', 'dist', 'build', '.git', 'locales'];
const EXCLUDE_FILES = [];

// 解析命令行参数
const isFix = process.argv.includes('--fix');

/**
 * 递归获取目录下所有文件
 */
function getAllFiles(dir, extensions) {
    const files = [];

    function traverse(currentDir) {
        const items = fs.readdirSync(currentDir);

        for (const item of items) {
            const fullPath = path.join(currentDir, item);
            const stat = fs.statSync(fullPath);

            if (stat.isDirectory()) {
                // 排除不需要的目录
                if (!EXCLUDE_DIRS.includes(item)) {
                    traverse(fullPath);
                }
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
 * 移除代码中的注释
 * - 单行注释 // ...
 * - 多行注释 /* ... * /
 * - JSDoc 注释 /** ... * /
 */
function removeComments(code) {
    let result = '';
    let i = 0;
    const len = code.length;

    while (i < len) {
        // 检查字符串字面量（字符串中的中文不应该算硬编码）
        if (code[i] === '"' || code[i] === "'" || code[i] === '`') {
            const quote = code[i];
            result += code[i];
            i++;

            // 处理字符串内容
            while (i < len) {
                if (code[i] === '\\') {
                    // 转义字符
                    result += code[i];
                    i++;
                    if (i < len) {
                        result += code[i];
                        i++;
                    }
                } else if (code[i] === quote) {
                    result += code[i];
                    i++;
                    break;
                } else {
                    result += code[i];
                    i++;
                }
            }
            continue;
        }

        // 检查单行注释
        if (code[i] === '/' && code[i + 1] === '/') {
            // 跳过直到行尾
            while (i < len && code[i] !== '\n') {
                i++;
            }
            continue;
        }

        // 检查多行注释
        if (code[i] === '/' && code[i + 1] === '*') {
            i += 2;
            // 跳过直到 */
            while (i < len - 1) {
                if (code[i] === '*' && code[i + 1] === '/') {
                    i += 2;
                    break;
                }
                i++;
            }
            continue;
        }

        result += code[i];
        i++;
    }

    return result;
}

/**
 * 查找中文字符串
 * 匹配模式：
 * - '中文' 或 "中文" 或 `中文`
 * - 但排除已经使用 t() 包裹的情况
 */
function findChineseStrings(codeWithoutComments, originalCode, filePath) {
    const results = [];
    const lines = originalCode.split('\n');

    // 匹配字符串中的中文
    // 排除：t('...'), t("..."), t(`...`)
    // 排除：import 语句中的路径
    // 排除：console.log, console.error 等调试语句
    const chineseRegex = /['"`]([^'"`]*[\u4e00-\u9fa5]+[^'"`]*)['"`]/g;

    let match;
    while ((match = chineseRegex.exec(codeWithoutComments)) !== null) {
        const fullMatch = match[0];
        const content = match[1];
        const matchStart = match.index;

        // 获取匹配位置前后的上下文
        const beforeMatch = codeWithoutComments.slice(Math.max(0, matchStart - 30), matchStart);
        const afterMatch = codeWithoutComments.slice(matchStart + fullMatch.length, matchStart + fullMatch.length + 10);

        // 排除情况

        // 1. 已经被 t() 包裹
        if (/\bt\s*\(\s*$/.test(beforeMatch)) {
            continue;
        }

        // 2. import 语句
        if (/\bimport\s*$/.test(beforeMatch) || /\bfrom\s*$/.test(beforeMatch)) {
            continue;
        }

        // 3. console.log 等调试语句（可选，根据需要开启）
        // if (/console\.\w+\s*\(\s*$/.test(beforeMatch)) {
        //     continue;
        // }

        // 4. 类型定义中的 key（如 '中文': ...）
        if (/\]\s*:\s*$/.test(beforeMatch) || /['"`]\s*,\s*$/.test(beforeMatch)) {
            continue;
        }

        // 5. 纯数字或标点（不是纯中文文本）
        if (!/[\u4e00-\u9fa5]/.test(content)) {
            continue;
        }

        // 找到原始代码中的行号
        const beforeText = codeWithoutComments.slice(0, matchStart);
        const lineNum = (beforeText.match(/\n/g) || []).length + 1;
        const lineContent = lines[lineNum - 1] || '';

        // 计算列位置
        const lastNewlineIndex = beforeText.lastIndexOf('\n');
        const colNum = matchStart - lastNewlineIndex;

        results.push({
            file: filePath,
            line: lineNum,
            column: colNum,
            content: content.trim(),
            fullLine: lineContent.trim(),
        });
    }

    return results;
}

/**
 * 主函数
 */
function main() {
    console.log('=== 搜索硬编码中文字符串 ===\n');
    console.log(`模式: ${isFix ? '修复模式' : '检测模式'}\n`);

    const files = getAllFiles(SRC_DIR, EXTENSIONS);
    console.log(`正在搜索 ${files.length} 个文件...\n`);

    const allResults = [];
    let filesWithIssues = 0;

    for (const file of files) {
        const code = fs.readFileSync(file, 'utf-8');

        // 移除注释
        const codeWithoutComments = removeComments(code);

        // 查找中文
        const results = findChineseStrings(codeWithoutComments, code, file);

        if (results.length > 0) {
            filesWithIssues++;
            allResults.push(...results);
        }
    }

    if (allResults.length === 0) {
        console.log('✅ 没有发现硬编码的中文字符串！');
        return;
    }

    // 按文件分组显示结果
    console.log(`发现 ${allResults.length} 处硬编码中文:\n`);
    console.log('─'.repeat(80));

    // 按文件分组
    const groupedResults = {};
    for (const result of allResults) {
        const relativePath = path.relative(SRC_DIR, result.file);
        if (!groupedResults[relativePath]) {
            groupedResults[relativePath] = [];
        }
        groupedResults[relativePath].push(result);
    }

    // 输出结果
    for (const [file, results] of Object.entries(groupedResults)) {
        console.log(`\n📄 ${file}`);
        console.log('─'.repeat(60));

        for (const result of results) {
            console.log(`   L${result.line}: "${result.content}"`);
            // 显示完整行（可选）
            // console.log(`        ${result.fullLine.slice(0, 80)}${result.fullLine.length > 80 ? '...' : ''}`);
        }
    }

    console.log('\n' + '─'.repeat(80));
    console.log(`\n统计:`);
    console.log(`  - 涉及文件: ${filesWithIssues} 个`);
    console.log(`  - 硬编码中文: ${allResults.length} 处`);

    // 输出详细统计
    console.log('\n按文件统计:');
    const sortedFiles = Object.entries(groupedResults).sort((a, b) => b[1].length - a[1].length);
    for (const [file, results] of sortedFiles.slice(0, 10)) {
        console.log(`  ${results.length.toString().padStart(3)} 处: ${file}`);
    }
    if (sortedFiles.length > 10) {
        console.log(`  ... 还有 ${sortedFiles.length - 10} 个文件`);
    }
}

// 运行
main();
