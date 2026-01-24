using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MementoMori.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationController : ControllerBase
    {
        private readonly LocalizationService _localizationService;
        private readonly ILogger<LocalizationController> _logger;

        public LocalizationController(LocalizationService localizationService, ILogger<LocalizationController> logger)
        {
            _localizationService = localizationService;
            _logger = logger;
        }

        [HttpGet("resources")]
        public IActionResult GetResources([FromQuery] string lang = "zhCN")
        {
            if (!Enum.TryParse<LanguageType>(lang, true, out var languageType))
            {
                languageType = LanguageType.zhCN;
            }

            _logger.LogInformation("Request for localization resources: {Lang}", languageType);
            var resources = _localizationService.GetResources(languageType);
            
            return Ok(resources);
        }
    }
}
