using Microsoft.Playwright;
using PlaywrightTest.Pages;
using PlaywrightTest.Helpers;

namespace PlaywrightTest.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class GoogleSearchTests : TestSetup
    {
        [Test]
        [TestCase("chromium")]
        [TestCase("webkit")]
        [TestCase("firefox")]
        public async Task SearchAndNavigateToPlaywrightDocs(string browserType)
        {
            var page = await Playwright.CreatePageAsync(browserType, new BrowserTypeLaunchOptions { Headless = false });

            var googleHomePage = new GoogleHomePage(page);
            var googleSearchResultsPage = new GoogleSearchResultsPage(page);
            var playwrightHomePage = new PlaywrightHomePage(page);
            var playwrightDocsPage = new PlaywrightDocsPage(page);

            await page.GotoAsync("https://www.google.pl");

            await googleHomePage.AcceptCookiesAsync();

            await page.RunAndWaitForNavigationAsync(async () => await googleHomePage.SearchAsync("playwright documentation"));

            var numberOfResults = await googleSearchResultsPage.GetNumberOfResultsAsync();
            Assert.IsTrue(numberOfResults > 0, "No search results found.");

            await page.RunAndWaitForNavigationAsync(async () => await googleSearchResultsPage.ClickFirstResultAsync());

            await page.WaitForLoadStateAsync(LoadState.Load);

            Assert.That(page.Url, Is.EqualTo("https://playwright.dev/"), $"Did not navigate to the Playwright docs website. Navigated to link: {page.Url}");

            await page.RunAndWaitForNavigationAsync(async () => await playwrightHomePage.ClickDocsLinkAsync());

            Assert.IsTrue(await playwrightDocsPage.IsVisibleInstallationLinkAsync(), "Installation link is not visible.");
            Assert.IsTrue(await playwrightDocsPage.IsVisibledWritingTestsLinkAsync(), "Writing tests link is not visible.");
            Assert.IsTrue(await playwrightDocsPage.IsVisibleTraceViewerLinkAsync(), "Trace viewer link is not visible.");
        }
    }
}
