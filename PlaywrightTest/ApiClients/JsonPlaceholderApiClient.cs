using Microsoft.Playwright;
using Newtonsoft.Json;

namespace PlaywrightTests.Clients
{
    public class JsonPlaceholderApiClient
    {
        private readonly IAPIRequestContext _request;
        private readonly string _apiBaseUrl;

        public JsonPlaceholderApiClient(IAPIRequestContext request, string apiBaseUrl)
        {
            _request = request;
            _apiBaseUrl = apiBaseUrl;
        }

        public async Task<IAPIResponse> CreateUserAsync(object newUser)
        {
            return await _request.PostAsync(_apiBaseUrl + "users", new APIRequestContextOptions
            {
                DataObject = newUser,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json; charset=UTF-8" }
                }
            });
        }
        public async Task<IAPIResponse> GetUsersAsync()
        {
            return await _request.GetAsync(_apiBaseUrl + "users");
        }
    }
}
