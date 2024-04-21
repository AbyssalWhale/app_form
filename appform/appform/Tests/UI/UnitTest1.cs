using appform.Tests.Fixtures;

namespace appform.Tests.UI
{
    public class Tests : TestsCoreFixtures
    {
        [Test]
        public async Task Test1()
        {
            Assert.Pass();
            await Task.Delay(5000);
        }
    }
}