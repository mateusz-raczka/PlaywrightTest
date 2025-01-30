﻿using Microsoft.Playwright;

namespace PlaywrightTests.Tests
{
    public abstract class ApiTestSetup : PlaywrightTest
    {
        protected IAPIRequestContext _request = null!;
        protected abstract string APIBaseUrl { get; }

        [SetUp]
        public async Task SetUp()
        {
            try
            {
                _request = await Playwright.APIRequest.NewContextAsync(new()
                {
                    BaseURL = APIBaseUrl,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set up API request context: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public async Task TearDown()
        {
            await _request.DisposeAsync();
        }
    }
}