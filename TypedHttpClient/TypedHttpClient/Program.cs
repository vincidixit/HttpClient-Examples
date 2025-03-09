using TypedHttpClient.Services.Abstraction;
using TypedHttpClient.Services.Implementation;

namespace TypedHttpClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddHttpClient<IGithubService, GithubService>(configure =>
            {
                configure.BaseAddress = new Uri("https://api.github.com/");
                configure.DefaultRequestHeaders.Add("Accept", "application/json");
                configure.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
