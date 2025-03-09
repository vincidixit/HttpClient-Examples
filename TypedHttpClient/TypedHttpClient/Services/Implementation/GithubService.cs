using System.Net.Http;
using System.Text.Json;
using TypedHttpClient.Services.Abstraction;
using TypedHttpClient.Services.Types;

namespace TypedHttpClient.Services.Implementation
{
    public class GithubService : IGithubService
    {
        private readonly HttpClient _httpClient;
        public GithubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetGithubUserDetails(string username)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/users/{username}");

                // Throw an exception if the API response is not successful (not in 200-299 range)
                response.EnsureSuccessStatusCode();

                var stringContent = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<User>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Exception occurred while retrieving user details from Github API. Exception Message: {ex.Message}");
            }

            return null;
        }
    }
}
