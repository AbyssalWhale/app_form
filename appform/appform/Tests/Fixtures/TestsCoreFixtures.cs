using appform.Models.UI.POM;
using Bogus;
using Microsoft.Playwright;

namespace appform.Tests.Fixtures
{
    [TestFixture]
    public class TestsCoreFixtures
    {
        protected IPlaywright PlaywrightTests;
        protected IBrowser Browser;
        protected IPage Page;
        protected HomePage HomePage;
        protected Faker Faker;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            PlaywrightTests = await Playwright.CreateAsync();
            Browser = await PlaywrightTests.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Args = new[] { "--start-maximized" },
                Headless = false,
            });
            Faker = new Faker();
        }

        [SetUp]
        public async Task Setup()
        {
            HomePage = new HomePage(page: await Browser.NewPageAsync());
            await HomePage.Navigate();
        }

        [TearDown]
        public async Task TearDown()
        {
            await HomePage.CloseBrowser();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await Browser.DisposeAsync();
        }
    }
}
