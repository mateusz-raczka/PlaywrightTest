using Microsoft.Playwright;
using PlaywrightTests.Config;

namespace PlaywrightTests.Helpers
{
    public static class PlaywrightExtensions
    {
        private static async Task<IBrowser> LaunchBrowserAsync(this IPlaywright playwright, string browserType, BrowserTypeLaunchOptions browserTypeLaunchOptions)
        {
            return browserType.ToLower() switch
            {
                "chromium" => await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions),
                "firefox" => await playwright.Firefox.LaunchAsync(browserTypeLaunchOptions),
                "webkit" => await playwright.Webkit.LaunchAsync(browserTypeLaunchOptions),
                _ => throw new ArgumentException($"Unsupported browser type: {browserType}")
            };
        }
        public static async Task<IPage> CreatePageAsync(this IPlaywright playwright, string browserType)
        {
            var settings = ConfigManager.PlaywrightSettings;
            var browserTypeLaunchOptions = new BrowserTypeLaunchOptions
            {
                Headless = settings.Headless,
                SlowMo = settings.SlowMo
            };
            var browser = await playwright.LaunchBrowserAsync(browserType, browserTypeLaunchOptions);
            var browserNewContextOptions = new BrowserNewContextOptions
            {
                ViewportSize = new()
                {
                    Width = settings.Viewport.Width,
                    Height = settings.Viewport.Height
                },
            };
            var context = await browser.NewContextAsync(browserNewContextOptions);
            return await context.NewPageAsync();
        }
    }
}
