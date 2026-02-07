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

        public IReadOnlyDictionary<string, string> GetResources(LanguageType lang)
        {
            try
            {
                _logger.LogInformation("Fetching resources for language: {Lang}", lang);
                
                // 设置语言
                Masters.TextResourceTable.SetLanguageType(lang);
                
                // 加载资源
                if (Masters.TextResourceTable.Load())
                {
                    return Masters.TextResourceTable.AllResources;
                }
                
                return new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting resources for {Lang}", lang);
                return new Dictionary<string, string>();
            }
        }

        public LocalizationManifest GetManifest(LanguageType lang)
        {
            var resources = GetResources(lang);
            var json = JsonSerializer.Serialize(resources);
            var hash = ComputeHash(json);
            
            return new LocalizationManifest
            {
                Language = lang.ToString(),
                Hash = hash,
                Count = resources.Count,
                LastUpdated = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        private string ComputeHash(string input)
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
