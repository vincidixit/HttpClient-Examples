using BasicHttpClient.Services.Types;

namespace BasicHttpClient.Services.Abstraction
{
    public interface IGithubService
    {
        Task<User> GetGithubUserDetails(string username);
    }
}
