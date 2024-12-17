using Microsoft.Playwright;

namespace PlaywrightTest.Pages
{
    public class GoogleHomePage : PageBase
    {
        public GoogleHomePage(IPage page) : base(page) { }

        #region Locators
        private ILocator SearchTextInput => _page.Locator("[aria-label='Szukaj']");
        private ILocator AcceptCookiesButton => _page.Locator("#L2AGLb");
        private ILocator SearchButton => _page.Locator("[aria-label='Szukaj w Google']").First;
        #endregion

        #region Methods
        public async Task AcceptCookiesAsync() => await AcceptCookiesButton.ClickAsync();

        public async Task SearchAsync(string query)
        {
            await SearchTextInput.FillAsync(query);
            await SearchButton.ClickAsync();
        }
        #endregion
    }
}
