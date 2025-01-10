using Microsoft.Playwright;
using PlaywrightTest.Pages;
using PlaywrightTest.Helpers;
using System.Net;

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
            //this should be in test setup class
            var page = await Playwright.CreatePageAsync(browserType, new BrowserTypeLaunchOptions { Headless = true });

            //this also should be in test setup class
            var googleHomePage = new GoogleHomePage(page);
            var googleSearchResultsPage = new GoogleSearchResultsPage(page);
            var playwrightHomePage = new PlaywrightHomePage(page);
            var playwrightDocsPage = new PlaywrightDocsPage(page);

            //maybe you should have here some custom go to method with extra validation that page was opened
            await page.GotoAsync("https://www.google.pl");

            //here you are expecting dialog just after page will be loaded, what in case if there will be no cookie dialog
            //better to use add locator handler
            await googleHomePage.AcceptCookiesAsync();

            //this is nice
            //but maybe better to have RunAndWaitForNavigationAsync inside method SearchAsync
            await page.RunAndWaitForNavigationAsync(async () => await googleHomePage.SearchAsync("playwright documentation"));

            var numberOfResults = await googleSearchResultsPage.GetNumberOfResultsAsync();
            Assert.IsTrue(numberOfResults > 0, "No search results found.");

            //same comment like for SearchAsync method
            await page.RunAndWaitForNavigationAsync(async () => await googleSearchResultsPage.ClickFirstResultAsync());

            Assert.That(page.Url, Is.EqualTo("https://playwright.dev/"), $"Did not navigate to the Playwright docs website. Navigated to link: {page.Url}");

            //same here
            await page.RunAndWaitForNavigationAsync(async () => await playwrightHomePage.ClickDocsLinkAsync());

            //use multiple assertions scope
            //I think this is really good place for aria snapshots validation
            Assert.IsTrue(await playwrightDocsPage.IsVisibleInstallationLinkAsync(), "Installation link is not visible.");
            Assert.IsTrue(await playwrightDocsPage.IsVisibledWritingTestsLinkAsync(), "Writing tests link is not visible.");
            Assert.IsTrue(await playwrightDocsPage.IsVisibleTraceViewerLinkAsync(), "Trace viewer link is not visible.");
        }
    }
}

/*Refactor:
NonHeadless mode does not work
Add config file(with all urls env, browsers, credentials, headless mode)*/