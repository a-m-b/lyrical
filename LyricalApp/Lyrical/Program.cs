using Lyrical.Core.Artists.Repositories;
using Lyrical.Core.Artists.Services;
using Lyrical.Core.Lyrics.Repositories;
using Lyrical.Core.Lyrics.Services;
using Lyrical.Infrastructure.LyricsOvh.Client;
using Lyrical.Infrastructure.Spotify.Client;
using Lyrical.Infrastructure.Spotify.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lyrical
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    services.AddHttpClient("Lyrics", c =>
                    {
                        c.BaseAddress = new Uri(configuration.GetSection($"LyricsOvh:Url").Value);
                        c.Timeout = new TimeSpan(0, 0, 10);
                    });

                    services.AddHostedService<Worker>();
                    services.Configure<SpotifyConfiguration>(configuration.GetSection("Spotify"));
                    services.AddSingleton<IArtistsRepository, SpotifyClient>();
                    services.AddSingleton<ILyricsRepository, LyricsClient>();
                    services.AddSingleton<IArtistsService, ArtistsService>();
                    services.AddSingleton<ILyricsService, LyricsService>();
                });
    }
}