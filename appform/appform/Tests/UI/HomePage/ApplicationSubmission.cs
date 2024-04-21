using appform.Tests.Fixtures;
using FluentAssert;

namespace appform.Tests.UI.HomePage
{
    public class ApplicationSubmission : TestsCoreFixtures
    {
        private string FirstName;
        private string LastName;
        private string Email;
        private string Password;
        private string PasswordConfirmed;

        [SetUp]
        public async Task Setup()
        {
            // Arrange
            FirstName = Faker.Name.FirstName();
            LastName = Faker.Name.LastName();
            Email = Faker.Person.Email;
            Password = Faker.Internet.Password();
            PasswordConfirmed = Password;
        }

        [Test]
        [Description("Automate the form submission with valid data and verify that the submission is successful and the data is correctly displayed on the success page.")]
        public async Task TestCase1()
        {
            // Act
            await HomePage.InputFirstName(FirstName);
            await HomePage.InputLastName(LastName);
            await HomePage.InputEmail(Email);
            await HomePage.InputPassword(Password);
            await HomePage.InputConfirmPassword(PasswordConfirmed);
            await HomePage.UnlockSlider();
            var formSubmissionsPage = await HomePage.ClickSubmitButton();

            // Assert 
            var isTitleMatched = await formSubmissionsPage.IsTitleMatchedWithExpected();
            isTitleMatched.ShouldBeTrue($"page title is not matching with expected: {formSubmissionsPage.Title}");
            var isHeaderDisplayed = await formSubmissionsPage.IsHeaderDisplayed();
            isHeaderDisplayed.ShouldBeEqualTo(true);

            var firstNameActual = await formSubmissionsPage.GetName();
            var emailActual = await formSubmissionsPage.GetEmail();
            firstNameActual.ShouldBeEqualTo($"{FirstName} {LastName}");
            emailActual.ShouldBeEqualTo(Email);
        }

        [Test]
        [Description("Automate form submission with invalid data (e.g., mismatched passwords, invalid email format) and verify that the submission fails and appropriate validation messages are displayed.\r\n")]
        [TestCase("", null, null, null, null)]
        [TestCase(null, "", null, null, null)]
        [TestCase(null, null, "usergmail.com", null, null)]
        [TestCase(null, null, null, "123123123", null)]
        [TestCase(null, null, null, null, "123123123")]
        public async Task TestCase2(string? firstName, string? lastName, string? email, string? pass, string? passConfirmed)
        {
            // Arrange
            var firstNameExpected = firstName is null ? FirstName : firstName;
            var lastNameExpected = lastName is null ? LastName : lastName;
            var emailExpected = email is null ? Email : email;
            var passwordExpected = pass is null ? Password : pass;
            var passwordConfirmedExpected = passConfirmed is null ? PasswordConfirmed : passConfirmed;

            // Act
            await HomePage.InputFirstName(firstNameExpected);
            await HomePage.InputLastName(lastNameExpected);
            await HomePage.InputEmail(emailExpected);
            await HomePage.InputPassword(passwordExpected);
            await HomePage.InputConfirmPassword(passwordConfirmedExpected);
            await HomePage.UnlockSlider();
            var formSubmissionsPage = await HomePage.ClickSubmitButton();

            // Assert 
            var isTitleMatched = await formSubmissionsPage.IsTitleMatchedWithExpected();
            isTitleMatched.ShouldBeFalse($"expected page title: {HomePage.Title} after submitting form with missing required property");
            if (pass is not null | passConfirmed is not null)
            {
                var isVisisble = await HomePage.IsPassNotMatchedErrorDisplayed();
                isVisisble.ShouldBeTrue("Expected that error is displayed on the page when passwords are mismatched in submitted application");
            }
        }

        [Test]
        [Description("Test the slider captcha functionality by attempting form submission without sliding the captcha to the end and verify that the form submission is blocked.\r\n")]
        public async Task TestCase3()
        {
            // Act
            await HomePage.InputFirstName(FirstName);
            await HomePage.InputLastName(LastName);
            await HomePage.InputEmail(Email);
            await HomePage.InputPassword(Password);
            await HomePage.InputConfirmPassword(PasswordConfirmed);
            var formSubmissionsPage = await HomePage.ClickSubmitButton();

            // Assert
            var isTitleMatched = await formSubmissionsPage.IsTitleMatchedWithExpected();
            isTitleMatched.ShouldBeFalse($"expected page title: {HomePage.Title} after submitting form with unsolved captcha");
            var isVisisble = await HomePage.IsUnsolvedCaptchaErrorDisplayed();
            isVisisble.ShouldBeTrue("Expected that error is displayed on the page after sumbitting application with unsolved captcha");
            await Console.Out.WriteLineAsync("");
        }
    }
}