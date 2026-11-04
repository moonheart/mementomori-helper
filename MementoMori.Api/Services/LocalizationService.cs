using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Enums;
using Microsoft.Extensions.Logging;

namespace MementoMori.Api.Services
{
    [RegisterScoped]
    [AutoConstructor]
    public partial class LocalizationService
    {
        private readonly ILogger<LocalizationService> _logger;

        /// <summary>
        /// 缓存项: 源文件的修改时间 + 合并后的词典 + manifest。
        /// </summary>
        private sealed record Entry(long Stamp, IReadOnlyDictionary<string, string> Resources, LocalizationManifest Manifest);

        /// <summary>
        /// 每个语言缓存一份。Master 数据由 MasterDataService 每小时覆盖一次,
        /// 所以把源文件的修改时间当版本号: 文件一变就重新读, 进程重启也会重新读。
        /// 最多 14 种语言 x ~30k 条, 内存上限是固定的。
        /// </summary>
        private static readonly ConcurrentDictionary<LanguageType, Entry> Cache = new();

        private static readonly object Gate = new();

        public IReadOnlyDictionary<string, string> GetResources(LanguageType lang)
        {
            return GetEntry(lang).Resources;
        }

        public LocalizationManifest GetManifest(LanguageType lang)
        {
            return GetEntry(lang).Manifest;
        }

        /// <summary>
        /// Master 数据更新后由 MasterDataService 调用。即使文件时间戳没变 (例如同一秒内替换),
        /// 下一次请求也会重新读一遍。
        /// </summary>
        public static void Invalidate()
        {
            Cache.Clear();
        }

        private Entry GetEntry(LanguageType lang)
        {
            var stamp = GetStamp(lang);
            if (Cache.TryGetValue(lang, out var cached) && cached.Stamp == stamp) return cached;

            lock (Gate)
            {
                if (Cache.TryGetValue(lang, out cached) && cached.Stamp == stamp) return cached;

                var entry = Build(lang, stamp);

                // 读失败 (文件正在被覆盖, 或语言包不存在) 时不缓存, 下次请求再试
                if (entry.Resources.Count > 0) Cache[lang] = entry;

                return entry;
            }
        }

        /// <summary>
        /// 语言自己的文件 + 英文文件 (合并兜底用) 里最新的修改时间。
        /// 文件不存在时返回 0, 这样恢复文件后会自动重读。
        /// </summary>
        private static long GetStamp(LanguageType lang)
        {
            var stamp = GetLastWriteTicks(lang);
            if (lang != LanguageType.enUS) stamp = Math.Max(stamp, GetLastWriteTicks(LanguageType.enUS));
            return stamp;
        }

        private static long GetLastWriteTicks(LanguageType lang)
        {
            try
            {
                var path = Path.Combine("Master", MementoMori.Ortega.Share.Master.Table.TextResourceTable.GetMbName(lang));
                return File.Exists(path) ? File.GetLastWriteTimeUtc(path).Ticks : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private Entry Build(LanguageType lang, long stamp)
        {
            var resources = ReadAllOrNull(lang) ?? new Dictionary<string, string>();

            // 部分语言的游戏文本有空条目或整段缺失, 用英文补上, 否则前端显示空白
            if (lang != LanguageType.enUS)
            {
                var english = ReadAllOrNull(LanguageType.enUS);
                if (english != null)
                {
                    foreach (var (key, text) in english)
                    {
                        if (!resources.TryGetValue(key, out var value) || string.IsNullOrWhiteSpace(value))
                        {
                            resources[key] = text;
                        }
                    }
                }
            }

            var json = JsonSerializer.Serialize(resources);
            var manifest = new LocalizationManifest
            {
                Language = lang.ToString(),
                Hash = ComputeHash(json),
                Count = resources.Count,
                LastUpdated = stamp > 0 ? new DateTimeOffset(new DateTime(stamp, DateTimeKind.Utc)).ToUnixTimeMilliseconds() : DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            return new Entry(stamp, resources, manifest);
        }

        private Dictionary<string, string>? ReadAllOrNull(LanguageType lang)
        {
            try
            {
                return Masters.TextResourceTable.ReadAll(lang);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading text resources for {Lang}", lang);
                return null;
            }
        }

        private static string ComputeHash(string input)
        {
            using var md5 = MD5.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = md5.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }

    public class LocalizationManifest
    {
        public string Language { get; set; }
        public string Hash { get; set; }
        public int Count { get; set; }
        public long LastUpdated { get; set; }
    }
}
