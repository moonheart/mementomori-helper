/**
 * 将 PascalCase 字符串转换为 camelCase
 * @example
 * pascalToCamelCase('BattleEndInfo') // => 'battleEndInfo'
 * pascalToCamelCase('HP') // => 'hp'
 * pascalToCamelCase('PlayerId') // => 'playerId'
 */
export function pascalToCamelCase(str: string): string {
    if (!str) return str;
    // 首字母小写
    return str.charAt(0).toLowerCase() + str.slice(1);
}

/**
 * 递归转换对象的所有 key 从 PascalCase 到 camelCase
 * 支持嵌套对象和数组
 * 
 * @example
 * const result = convertKeysToCamelCase({
 *   BattleEndInfo: { IsWin: true },
 *   PlayerList: [{ PlayerId: 1 }]
 * });
 * // => { battleEndInfo: { isWin: true }, playerList: [{ playerId: 1 }] }
 */
export function convertKeysToCamelCase<T>(obj: unknown): T {
    // 处理 null 和 undefined
    if (obj === null || obj === undefined) {
        return obj as T;
    }

    // 处理数组
    if (Array.isArray(obj)) {
        return obj.map(item => convertKeysToCamelCase(item)) as T;
    }

    // 处理原始类型
    if (typeof obj !== 'object') {
        return obj as T;
    }

    // 处理对象
    const result: Record<string, unknown> = {};
    for (const [key, value] of Object.entries(obj)) {
        const camelKey = pascalToCamelCase(key);
        result[camelKey] = convertKeysToCamelCase(value);
    }

    return result as T;
}
