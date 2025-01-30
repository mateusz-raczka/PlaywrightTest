using PlaywrightTests.Config;
using PlaywrightTests.ApiClients;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class SpaceXApiTests : ApiTestSetup
    {
        private SpaceXApiClient _spaceXApiClient;

        protected override string APIBaseUrl => ConfigManager.GetAPITestBaseUrl(typeof(SpaceXApiClient).Name);

        [SetUp]
        public void SetUp()
        {
            _spaceXApiClient = new SpaceXApiClient(_request, APIBaseUrl);
        }

        [Test]
        public async Task CheckIfRocketFromLatestLaunchExists()
        {
            var latestRocketLaunchResponse = await _spaceXApiClient.GetLatestLaunchAsync();
            Assert.IsTrue(latestRocketLaunchResponse.Ok, "Failed to fetch latest rocket launch.");

            var latestRocketLaunchJson = await latestRocketLaunchResponse.JsonAsync();
            var latestLaunchRocketId = latestRocketLaunchJson.Value.GetProperty("rocket").ToString();
            Assert.IsNotNull(latestLaunchRocketId, "Rocket ID from latest launch is null.");

            var rocketDetailsResponse = await _spaceXApiClient.GetRocketByIdAsync(latestLaunchRocketId!);
            Assert.IsTrue(rocketDetailsResponse.Ok, $"Failed to fetch rocket details for ID {latestLaunchRocketId}.");

            var rocketDetailsJson = await rocketDetailsResponse.JsonAsync();
            var rocketId = rocketDetailsJson.Value.GetProperty("id").ToString();

            Assert.AreEqual(latestLaunchRocketId, rocketId, $"Expected rocket with ID {latestLaunchRocketId}, but got {rocketId}.");
        }
    }
}
