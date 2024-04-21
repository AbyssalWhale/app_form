using Microsoft.Playwright;

namespace appform.Models.UI.POM
{
    public abstract class PageBase
    {
        protected IPage Page;
        public abstract string Title { get; }

        public PageBase(IPage page)
        {
            Page = page;
        }

        public virtual async Task<bool> IsTitleMatchedWithExpected()
        {
            var title = await Page.TitleAsync();
            return title == Title;
        }

        public virtual async Task CloseBrowser()
        {
            await Page.CloseAsync();
        }
    }
}