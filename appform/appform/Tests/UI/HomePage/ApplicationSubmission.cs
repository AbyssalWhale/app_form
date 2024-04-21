using appform.Tests.Fixtures;
using FluentAssert;

namespace appform.Tests.UI.HomePage
{
    public class ApplicationSubmission : TestsCoreFixtures
    {
        [Test]
        [Description("Automate the form submission with valid data and verify that the submission is successful and the data is correctly displayed on the success page.")]
        public async Task TestCase1()
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