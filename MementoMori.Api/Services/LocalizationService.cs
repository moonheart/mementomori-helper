using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Enums;
using Microsoft.Extensions.Logging;

namespace MementoMori.Api.Services
{
    public class LocalizationService
    {
        private readonly ILogger<LocalizationService> _logger;

        public LocalizationService(ILogger<LocalizationService> logger)
        {
            _logger = logger;
        }

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
    }
}
