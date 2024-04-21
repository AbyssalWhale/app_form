using appform.Tests.Fixtures;

namespace appform.Tests.UI
{
    public class Tests : TestsCoreFixtures
    {
        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync(url: "https://qa-task.redvike.rocks/");
        }

        [Test]
        public async Task Test1()
        {
            Assert.Pass();
            await Task.Delay(5000);
        }
    }
}