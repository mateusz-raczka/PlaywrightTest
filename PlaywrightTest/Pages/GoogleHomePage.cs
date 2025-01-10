using Microsoft.Playwright;

namespace PlaywrightTest.Pages
{
    public class GoogleHomePage : PageBase
    {
        public GoogleHomePage(IPage page) : base(page)
        {
        }

        #region Locators

        //if there is an option always better to use aria locators (this is recommended by PW team) GetByLabel("Szukaj", new() { Exact = true })
        private ILocator SearchTextInput => _page.Locator("[aria-label='Szukaj']");

        private ILocator AcceptCookiesButton => _page.Locator("#L2AGLb");
        private ILocator SearchButton => _page.Locator("[aria-label='Szukaj w Google']").First;

        #endregion Locators

        #region Methods

        public async Task AcceptCookiesAsync() => await AcceptCookiesButton.ClickAsync();

        public async Task SearchAsync(string query)
        {
            await SearchTextInput.FillAsync(query);
            await SearchButton.ClickAsync();
        }

        #endregion Methods
    }
}