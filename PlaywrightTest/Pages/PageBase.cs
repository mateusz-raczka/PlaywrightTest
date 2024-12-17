using Microsoft.Playwright;

namespace PlaywrightTest.Pages
{
    public class PageBase
    {
        protected readonly IPage _page;

        public PageBase(IPage page)
        {
            _page = page;
        }
    }
}
