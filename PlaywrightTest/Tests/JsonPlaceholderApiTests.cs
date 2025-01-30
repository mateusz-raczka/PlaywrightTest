using PlaywrightTests.Clients;
using PlaywrightTests.Config;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class JsonPlaceholderApiTests : ApiTestSetup
    {
        protected override string APIBaseUrl => ConfigManager.GetAPITestBaseUrl(typeof(JsonPlaceholderApiClient).Name);

        private JsonPlaceholderApiClient _jsonPlaceholderApiClient;

        [SetUp]
        public void Setup()
        {
            _jsonPlaceholderApiClient = new JsonPlaceholderApiClient(_request, APIBaseUrl);
        }

        [Test]
        public async Task CheckIfJsonPlaceholderApiIsResponding()
        {
            var getUsersResponse = await _jsonPlaceholderApiClient.GetUsersAsync();

            Assert.IsTrue(getUsersResponse.Ok, "JsonPlaceholder API is not responding");
        }

        [Test]
        public async Task CheckIfNewUserIsOnListOfUsers()
        {
            var newUser = new
            {
                name = "Leanne Graham",
                username = "Bret",
                email = "Sincere@april.biz",
                address = new
                {
                    street = "Kulas Light",
                    suite = "Apt. 556",
                    city = "Gwenborough",
                    zipcode = "92998-3874",
                    geo = new
                    {
                        lat = "-37.3159",
                        lng = "81.1496",
                    }
                },
                phone = "1-770-736-8031 x56442",
                website = "hildegard.org",
                company = new
                {
                    name = "Romaguera-Crona",
                    catchPhrase = "Multi-layered client-server neural-net",
                    bs = "harness real-time e-markets"
                }
            };

            var postResponse = await _jsonPlaceholderApiClient.CreateUserAsync(newUser);
            Assert.IsTrue(postResponse.Ok, "Failed to create a new user.");

            var createdUser = await postResponse.JsonAsync();
            var createdUserId = createdUser.Value.GetProperty("id").ToString();

            var getUsersResponse = await _jsonPlaceholderApiClient.GetUsersAsync();
            Assert.IsTrue(getUsersResponse.Ok, "Failed to retrieve users list.");

            var usersList = await getUsersResponse.JsonAsync();

            bool userExists = usersList.Value.EnumerateArray().Any(u =>
                u.GetProperty("id").ToString() == createdUserId &&
                u.GetProperty("username").ToString() == newUser.username);

            Assert.IsTrue(userExists, $"User {newUser.username} with ID {createdUserId} not found in users list.");
        }
    }
}
