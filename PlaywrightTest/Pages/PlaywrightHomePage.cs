using Microsoft.Playwright;
using PlaywrightTests.Helpers;

namespace PlaywrightTests.Pages
{
    public class PlaywrightHomePage : PageBase
    {
        public PlaywrightHomePage(IPage page) : base(page) { }

        #region Locators
        private ILocator DocsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Docs" });
        #endregion

        #region Methods
        public async Task ClickDocsLinkAsync()
        {
            await _page.RunAndWaitForNavigationAsync(async () => await DocsLink.ClickAsync());
        }
        #endregion
    }
}
