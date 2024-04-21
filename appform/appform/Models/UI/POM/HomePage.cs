using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.Common;

namespace appform.Models.UI.POM
{
    public class HomePage : PageBase
    {
        public override string Title => "Recruitment Task - Web Form";

        public HomePage(IPage page) : base(page)
        {
            
        }

        public async Task<HomePage> Navigate()
        {
            await Page.GotoAsync(url: "https://qa-task.redvike.rocks/");
            await IsAtPage();
            return this;
        }
    }
}
