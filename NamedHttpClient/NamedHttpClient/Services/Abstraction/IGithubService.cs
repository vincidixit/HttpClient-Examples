using NamedHttpClient.Services.Types;

namespace NamedHttpClient.Services.Abstraction
{
    public interface IGithubService
    {
        Task<User> GetGithubUserDetails(string username);
    }
}
