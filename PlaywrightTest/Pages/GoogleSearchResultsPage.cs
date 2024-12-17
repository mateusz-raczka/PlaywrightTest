using Microsoft.Playwright;

namespace PlaywrightTest.Pages
{
    public class GoogleSearchResultsPage : PageBase
    {
        public GoogleSearchResultsPage(IPage page) : base(page) { }

        #region Locators
        private ILocator SearchResultLinks => _page.Locator("#search a[jsname='UWckNb']");
        #endregion

        #region Methods
        public async Task<int> GetNumberOfResultsAsync() => await SearchResultLinks.CountAsync();
        public async Task ClickFirstResultAsync() => await SearchResultLinks.First.ClickAsync();
        #endregion
    }
}
