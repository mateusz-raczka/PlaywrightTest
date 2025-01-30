using Microsoft.Playwright;
using PlaywrightTests.Config;

namespace PlaywrightTests.Helpers
{
    public static class NavigationHelper
    {
        public static string GetPageUrl<T>()
        {
            return ConfigManager.GetUrl(typeof(T).Name);
        }

        public static async Task<bool> GotoPageAsync<T>(this IPage page)
        {
            var url = ConfigManager.GetUrl(typeof(T).Name);

            if (string.IsNullOrEmpty(url))
            {
                throw new Exception($"{typeof(T).Name} page URL not found in configuration.");
            }

            await page.GotoAsync(url);
            await page.WaitForURLAsync(url);

            var isRedirected = page.Url == url;

            return isRedirected;
        }
    }
}