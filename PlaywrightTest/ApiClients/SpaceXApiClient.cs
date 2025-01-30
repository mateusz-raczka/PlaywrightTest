using Microsoft.Playwright;

namespace PlaywrightTests.ApiClients
{
    public class SpaceXApiClient
    {
        private readonly IAPIRequestContext _request;
        private readonly string _apiBaseUrl;

        public SpaceXApiClient(IAPIRequestContext request, string apiBaseUrl)
        {
            _request = request;
            _apiBaseUrl = apiBaseUrl;
        }

        private const string GetLatestLaunchEndpoint = "launches/latest";
        private string GetRocketByIdEndpoint(string id) => $"rockets/{id}";

        public async Task<IAPIResponse> GetLatestLaunchAsync()
        {
            return await _request.GetAsync(_apiBaseUrl + GetLatestLaunchEndpoint);
        }

        public async Task<IAPIResponse> GetRocketByIdAsync(string rocketId)
        {
            return await _request.GetAsync(_apiBaseUrl + GetRocketByIdEndpoint(rocketId));
        }
    }
}
