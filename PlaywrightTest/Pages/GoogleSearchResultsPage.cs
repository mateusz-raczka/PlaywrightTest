using Microsoft.Playwright;
using PlaywrightTests.Helpers;

namespace PlaywrightTests.Pages
{
    public class GoogleSearchResultsPage : PageBase
    {
        public GoogleSearchResultsPage(IPage page) : base(page) { }

        #region Locators
        private ILocator SearchResultLinks => _page.Locator("#search a[jsname='UWckNb']");
        private ILocator BackToHomePageButton => _page.GetByRole(AriaRole.Link, new() { Name = "Przejdź do strony głównej" });
        #endregion

        #region Methods
        public async Task<int> GetNumberOfResultsAsync()
        {
            await SearchResultLinks.First.WaitForAsync();
            return await SearchResultLinks.CountAsync();
        }
        public async Task ClickFirstResultAsync()
        {
            await _page.RunAndWaitForNavigationAsync(async () => await SearchResultLinks.First.ClickAsync(), new() { WaitUntil = WaitUntilState.NetworkIdle });
        }
        #endregion
    }
}
