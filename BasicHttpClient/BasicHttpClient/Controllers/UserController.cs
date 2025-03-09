using BasicHttpClient.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicHttpClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGithubService _githubService;

        public UserController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserDetails(string username)
        {
            var user = await _githubService.GetGithubUserDetails(username);
            return Ok(user);
        }
    }
}
