using PlaywrightTests.Helpers;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class GoogleSearchTests : TestSetup
    {
        private GoogleHomePage _googleHomePage;
        private GoogleSearchResultsPage _googleSearchResultsPage;
        private PlaywrightHomePage _playwrightHomePage;
        private PlaywrightDocsPage _playwrightDocsPage;

        [SetUp]
        public void SetUp()
        {
            _googleHomePage = new GoogleHomePage(Page);
            _googleSearchResultsPage = new GoogleSearchResultsPage(Page);
            _playwrightHomePage = new PlaywrightHomePage(Page);
            _playwrightDocsPage = new PlaywrightDocsPage(Page);
        }

        [Test]
        [TestCaseSource(nameof(Browsers))]
        public async Task SearchAndNavigateToPlaywrightDocs(string browserType)
        {
            var successfullyRedirected = await Page.GotoPageAsync<GoogleHomePage>();

            Assert.IsTrue(successfullyRedirected, "Did not successfully navigate to google home page");

            await _googleHomePage.AcceptCookiesAsync();

            await _googleHomePage.SearchAsync("playwright documentation");

            var numberOfResults = await _googleSearchResultsPage.GetNumberOfResultsAsync();
            Assert.IsTrue(numberOfResults > 0, "No search results found.");

            await _googleSearchResultsPage.ClickFirstResultAsync();

            Assert.That(Page.Url, Is.EqualTo(NavigationHelper.GetPageUrl<PlaywrightHomePage>()), $"Did not navigate to the Playwright docs website. Navigated to link: {Page.Url}");

            await _playwrightHomePage.ClickDocsLinkAsync();

            Assert.Multiple(async () =>
            {
                Assert.IsTrue(await _playwrightDocsPage.IsVisibleInstallationLinkAsync(), "Installation link is not visible.");
                Assert.IsTrue(await _playwrightDocsPage.IsVisibledWritingTestsLinkAsync(), "Writing tests link is not visible.");
                Assert.IsTrue(await _playwrightDocsPage.IsVisibleTraceViewerLinkAsync(), "Trace viewer link is not visible.");
            });
        }
    }
}
