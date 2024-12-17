using Microsoft.Playwright;

namespace PlaywrightTest.Pages
{
    public class PlaywrightHomePage : PageBase
    {
        public PlaywrightHomePage(IPage page) : base(page) { }

        #region Locators
        private ILocator DocsLink => _page.Locator("text=Docs");
        #endregion

        #region Methods
        public async Task ClickDocsLinkAsync()
        {
            await DocsLink.ClickAsync();
        }
        #endregion
    }
}
