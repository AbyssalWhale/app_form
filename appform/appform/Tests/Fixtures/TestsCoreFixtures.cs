using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appform.Tests.Fixtures
{
    [TestFixture]
    public class TestsCoreFixtures
    {
        protected IPlaywright PlaywrightTests;
        protected IBrowser Browser;
        protected IPage Page;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            PlaywrightTests = await Playwright.CreateAsync();
            Browser = await PlaywrightTests.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Args = new[] { "--start-maximized" },
                Headless = false,
            });
            Page = await Browser.NewPageAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await Browser.DisposeAsync();
        }
    }
}
