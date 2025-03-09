using NamedHttpClient.Services.Abstraction;
using NamedHttpClient.Services.Types;
using System.Text.Json;

namespace NamedHttpClient.Services.Implementation
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
                using(var httpClient = _httpClientFactory.CreateClient("GithubServiceClient"))
                {
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
