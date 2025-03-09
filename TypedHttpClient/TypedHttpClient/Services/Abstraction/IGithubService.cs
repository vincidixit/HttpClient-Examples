using TypedHttpClient.Services.Types;

namespace TypedHttpClient.Services.Abstraction
{
    public interface IGithubService
    {
        Task<User> GetGithubUserDetails(string username);
    }
}
