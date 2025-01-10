using Microsoft.Playwright;

namespace PlaywrightTest.Helpers
{
    //Really good browser factory
    public static class PlaywrightExtensions
    {
        public static async Task<IBrowser> LaunchBrowserAsync(this IPlaywright playwright, string browserType, BrowserTypeLaunchOptions browserTypeLaunchOptions)
        {
            return browserType.ToLower() switch
            {
                "chromium" => await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions),
                "firefox" => await playwright.Firefox.LaunchAsync(browserTypeLaunchOptions),
                "webkit" => await playwright.Webkit.LaunchAsync(browserTypeLaunchOptions),
                _ => throw new ArgumentException($"Unsupported browser type: {browserType}")
            };
        }

        public static async Task<IPage> CreatePageAsync(this IPlaywright playwright, string browserType, BrowserTypeLaunchOptions options)
        {
            var browser = await playwright.LaunchBrowserAsync(browserType, options);
            var context = await browser.NewContextAsync();
            return await context.NewPageAsync();
        }
    }
}