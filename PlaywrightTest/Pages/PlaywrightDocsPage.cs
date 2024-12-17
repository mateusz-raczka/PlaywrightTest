using Microsoft.Playwright;

namespace PlaywrightTest.Pages
{
    public class PlaywrightDocsPage : PageBase
    {
        public PlaywrightDocsPage(IPage page) : base(page) { }

        #region Locators
        private ILocator InstallationLink => _page.GetByRole(AriaRole.Link, new() { NameString = "Installation" });
        private ILocator WritingTestsLink => _page.GetByRole(AriaRole.Link, new() { NameString = "Writing tests" });
        private ILocator TraceViewerLink => _page.GetByRole(AriaRole.Link, new() { NameString = "Trace viewer" }).First;
        #endregion

        #region Methods
        public async Task<bool> IsVisibleInstallationLinkAsync()
        {
            await InstallationLink.WaitForAsync();
            
            return await InstallationLink.IsVisibleAsync();
        }
        public async Task<bool> IsVisibledWritingTestsLinkAsync()
        {
            await WritingTestsLink.WaitForAsync();

            return await WritingTestsLink.IsVisibleAsync();
        }
        public async Task<bool> IsVisibleTraceViewerLinkAsync()
        {
            await TraceViewerLink.WaitForAsync();

            return await TraceViewerLink.IsVisibleAsync();
        }
        #endregion
    }
}
