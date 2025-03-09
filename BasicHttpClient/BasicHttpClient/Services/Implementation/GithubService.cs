using BasicHttpClient.Services.Abstraction;
using BasicHttpClient.Services.Types;
using System.Text.Json;

namespace BasicHttpClient.Services.Implementation
{
    public class GithubService : IGithubService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GithubService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<User> GetGithubUserDetails(string username)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://api.github.com");

                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");

                    var response = await httpClient.GetAsync($"/users/{username}");

                    // Throw an exception if the API response is not successful (not in 200-299 range)
                    response.EnsureSuccessStatusCode();

                    var stringContent = await response.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<User>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Exception occurred while retrieving user details from Github API. Exception Message: {ex.Message}");
            }

            return null;
        }
    }
}
