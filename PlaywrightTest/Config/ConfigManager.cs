using Microsoft.Extensions.Configuration;

namespace PlaywrightTests.Config
{
    public static class ConfigManager
    {
        private static readonly IConfigurationRoot _config;
        public static PlaywrightConfig PlaywrightSettings { get; }

        static ConfigManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _config = builder.Build();
            PlaywrightSettings = _config.GetSection("PlaywrightSettings").Get<PlaywrightConfig>();
        }

        public static string GetUrl(string key) => _config[$"TestUrls:{key}"];
        public static string GetAPITestBaseUrl(string key) => _config[$"APIBaseUrls:{key}"];
    }
}