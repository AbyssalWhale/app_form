using FluentAssert;
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

        public virtual async Task IsAtPage()
        {
            var title = await Page.TitleAsync();
            title.ShouldBeEqualTo(Title);
        }
    }
}