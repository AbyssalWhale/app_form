using Microsoft.Playwright;
using System;
namespace appform.Models.UI.POM
{
    public class FormSubmissionsPage : PageBase
    {
        public override string Title => "Form Submissions";

        public FormSubmissionsPage(IPage page) : base(page)
        {
        }

        public async Task<bool> IsHeaderDisplayed()
        {
            var element = await Page.QuerySelectorAsync("//h1[text()='Successful Form Submissions']");
            return await element.IsVisibleAsync();
        }

        public async Task<string> GetName()
        {
            var result = await GetNameOrEmailValueByLabel(label: "Name");
            return result;
        }

        public async Task<string> GetEmail()
        {
            var result = await GetNameOrEmailValueByLabel(label: "Email");

            return result;
        }

        private async Task<string> GetNameOrEmailValueByLabel(string label)
        {
            var result = await Page.EvaluateAsync<string>($@"
            (label) => {{
                const xpath = `//strong[text()='{label}:']/following-sibling::text()[1]`;
                const element = document.evaluate(xpath, document, null, XPathResult.STRING_TYPE, null).stringValue;
                return element.trim();
            }}", label
            );

            return result;
        }
    }
}
