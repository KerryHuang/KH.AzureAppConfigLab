using Azure.Data.AppConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace KH.AzureAppConfigLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AzureAppConfigController : ControllerBase
    {
        private readonly ILogger<AzureAppConfigController> _logger;
        private readonly ConfigurationClient _client;

        public AzureAppConfigController(ILogger<AzureAppConfigController> logger, ConfigurationClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetAzureAppConfigs()
        {
            List<string> results = new List<string>();


            // 列出所有設定
            var selector = new SettingSelector { KeyFilter = SettingSelector.Any };
            await foreach (var config in _client.GetConfigurationSettingsAsync(selector))
            {
                // Process the configuration setting
                results.Add("Key: " + config.Key + ", Value: " + config.Value);
            }

            if (results.Count > 0)
            {
                return Ok(results);
            }

            return NotFound();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAzureAppConfig(string key)
        {
            var config = await _client.GetConfigurationSettingAsync(key);
            return Ok(config.Value.Value);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> PutAzureAppConfig(string key, string value)
        {
            await _client.SetConfigurationSettingAsync(key, value);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostAzureAppConfig(string key, string value)
        {
            await _client.SetConfigurationSettingAsync(key, value);
            return Ok();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteAzureAppConfig(string key)
        {
            await _client.SetConfigurationSettingAsync(key, "");
            return Ok();
        }
    }
}
