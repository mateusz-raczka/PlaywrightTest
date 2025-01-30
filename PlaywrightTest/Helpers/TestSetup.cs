using Microsoft.Playwright;
using PlaywrightTests.Config;
using PlaywrightDriver = Microsoft.Playwright.Playwright;

namespace PlaywrightTests.Helpers
{
    public class TestSetup
    {
        protected static readonly string[] Browsers = ConfigManager.PlaywrightSettings.Browsers;
        protected IPage Page;
        protected IPlaywright Playwright;

        [SetUp]
        public async Task GlobalSetUp()
        {
            Playwright = await PlaywrightDriver.CreateAsync();
            var browserType = TestContext.CurrentContext.Test.Arguments[0].ToString();

            Page = await Playwright.CreatePageAsync(browserType);
        }

        [OneTimeTearDown]
        public void GlobalOneTimeTearDown()
        {
            Playwright.Dispose();
        }
    }
}