using Microsoft.Playwright;
using PlaywrightTests.Helpers;

namespace PlaywrightTests.Pages
{
    public class GoogleHomePage : PageBase
    {
        public GoogleHomePage(IPage page) : base(page) { }

        #region Locators
        private ILocator SearchTextInput => _page.Locator("[aria-label='Szukaj']");
        private ILocator AcceptCookiesButton => _page.GetByRole(AriaRole.Button, new() { Name = "Zaakceptuj wszystko" });
        private ILocator SearchButton => _page.GetByLabel("Szukaj w Google").First;
        #endregion

        #region Methods
        public async Task AcceptCookiesAsync()
        {
            await _page.AddLocatorHandlerAsync(AcceptCookiesButton,async () =>
            {
                await AcceptCookiesButton.ClickAsync();
            });
        }

        public async Task SearchAsync(string query)
        {
            await SearchTextInput.FillAsync(query);

            await _page.RunAndWaitForNavigationAsync(async () => await SearchButton.ClickAsync());
        }
        #endregion
    }
}
