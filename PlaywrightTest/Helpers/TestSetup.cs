using Microsoft.Playwright;
using PlaywrightTests.Config;

namespace PlaywrightTests.Helpers
{
    public abstract class TestSetup : PlaywrightTest
    {
        protected static readonly string[] Browsers = ConfigManager.PlaywrightSettings.Browsers;
        protected IPage Page { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            var browserType = TestContext.CurrentContext.Test.Arguments[0].ToString();

            Page = await Playwright.CreatePageAsync(browserType);
        }
    }
}