using Microsoft.Playwright;
using PlaywrightDriver = Microsoft.Playwright.Playwright;

namespace PlaywrightTest.Helpers
{
    public class TestSetup
    {
        protected IPlaywright Playwright;

        [OneTimeSetUp]
        public async Task GlobalSetUp()
        {
            Playwright = await PlaywrightDriver.CreateAsync();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            Playwright.Dispose();
        }
    }
}
