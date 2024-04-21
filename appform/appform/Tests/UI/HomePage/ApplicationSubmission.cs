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
            await formSubmissionsPage.IsAtPage();
            var isHeaderDisplayed = await formSubmissionsPage.IsHeaderDisplayed();
            isHeaderDisplayed.ShouldBeEqualTo(true);

            var firstNameActual = await formSubmissionsPage.GetName();
            var emailActual = await formSubmissionsPage.GetEmail();
            firstNameActual.ShouldBeEqualTo($"{FirstName} {LastName}");
            emailActual.ShouldBeEqualTo(Email);
        }

        [Test]
        [Description("Automate form submission with invalid data (e.g., mismatched passwords, invalid email format) and verify that the submission fails and appropriate validation messages are displayed.\r\n")]
        public async Task TestCase2()
        {
            // Arrange
            var firstName = Faker.Name.FirstName();
            var lastName = Faker.Name.LastName();
            var email = Faker.Person.Email;
            var password = Faker.Internet.Password();
            var passwordConfirmed = password;

            // Act
            await HomePage.InputFirstName(firstName);
            await HomePage.InputLastName(lastName);
            await HomePage.InputEmail(email);
            await HomePage.InputPassword(password);
            await HomePage.InputConfirmPassword(passwordConfirmed);
            await HomePage.UnlockSlider();
            var formSubmissionsPage = await HomePage.ClickSubmitButton();

            // Assert 
            await formSubmissionsPage.IsAtPage();
            var isHeaderDisplayed = await formSubmissionsPage.IsHeaderDisplayed();
            isHeaderDisplayed.ShouldBeEqualTo(true);

            var firstNameActual = await formSubmissionsPage.GetName();
            var emailActual = await formSubmissionsPage.GetEmail();
            firstNameActual.ShouldBeEqualTo($"{firstName} {lastName}");
            emailActual.ShouldBeEqualTo(email);
        }
    }
}