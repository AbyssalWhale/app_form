using FluentAssert;
using Microsoft.Playwright;

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
            var currentTitle = await IsTitleMatchedWithExpected();
            currentTitle.ShouldBeTrue(errorMessage: $"page title is not matching with expected: {Title}");

            return this;
        }

        public async Task<HomePage> InputFirstName(string value)
        {

            var inputElement = await Page.QuerySelectorAsync("//input[@name='first_name']");
            await inputElement.FillAsync(value);

            return this;
        }

        public async Task<HomePage> InputLastName(string value)
        {

            var inputElement = await Page.QuerySelectorAsync("//input[@name='last_name']");
            await inputElement.FillAsync(value);

            return this;
        }

        public async Task<HomePage> InputEmail(string value)
        {

            var inputElement = await Page.QuerySelectorAsync("//input[@name='email']");
            await inputElement.FillAsync(value);

            return this;
        }

        public async Task<HomePage> InputPassword(string value)
        {

            var inputElement = await Page.QuerySelectorAsync("//input[@name='password']");
            await inputElement.FillAsync(value);

            return this;
        }

        public async Task<HomePage> InputConfirmPassword(string value)
        {

            var inputElement = await Page.QuerySelectorAsync("//input[@name='confirm_password']");
            await inputElement.FillAsync(value);

            return this;
        }

        public async Task<HomePage> UnlockSlider()
        {
            var element = await Page.QuerySelectorAsync("//form[@method='POST']");
            await Page.EvaluateAsync(@"(element) => {
                const inputElement = document.createElement('input');
                inputElement.setAttribute('type', 'hidden');
                inputElement.setAttribute('id', 'captcha_solved');
                inputElement.setAttribute('name', 'captcha_solved');
                inputElement.setAttribute('value', 'true');
                element.appendChild(inputElement);
            }", element);

            return this;
        }

        public async Task<FormSubmissionsPage> ClickSubmitButton()
        {
            var inputElement = await Page.QuerySelectorAsync("//input[@value='Submit']");
            await inputElement.ClickAsync();

            return  new FormSubmissionsPage(Page);
        }

        public async Task<bool> IsPassNotMatchedErrorDisplayed()
        {
            var element = await Page.QuerySelectorAsync("//li[text()='Passwords do not match!']");
            return await element.IsVisibleAsync();
        }
    }
}
