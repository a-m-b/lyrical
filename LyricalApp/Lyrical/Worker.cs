using Lyrical.Core.Artists.Exceptions;
using Lyrical.Core.Artists.Messages.Queries;
using Lyrical.Core.Artists.Services;
using Lyrical.Core.Lyrics.Exceptions;
using Lyrical.Core.Lyrics.Messages.Queries;
using Lyrical.Core.Lyrics.Services;
using Microsoft.Extensions.Hosting;

namespace Lyrical
{
    public class Worker : BackgroundService
    {
        private readonly IHost _host;
        private readonly IArtistsService _artistsService;
        private readonly ILyricsService _lyricsService;

        public Worker(IHost host, IArtistsService artistsService, ILyricsService lyricsService)
        {
            _host = host;
            _artistsService = artistsService;
            _lyricsService = lyricsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Enter an artist name:");

            var artistName = Console.ReadLine();

            try
            {
                var trackNames = await _artistsService.GetArtistsTracks(new GetArtistsSongs() { ArtistName = artistName });
                var averageWords = await _lyricsService.GetTrackWordAverage(new GetTrackLyricAverage() { ArtistName = artistName, Tracks = trackNames });

                Console.WriteLine($"The average word count is {averageWords}");
            }
            catch (ArtistNotProvidedException ex)
            {
                SendErrorMessage(ex.Message);
            }
            catch (ArtistNotFoundException ex)
            {
                SendErrorMessage(ex.Message);
            }
            catch (ArtistHasNoTracksException ex)
            {
                SendErrorMessage(ex.Message);
            }
            catch (NoLyricsFoundException ex)
            {
                SendErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                SendErrorMessage(ex.Message);
            }

            _host.StopAsync();
        }

        private void SendErrorMessage(string error)
        {
            Console.WriteLine("#### ERROR ####");
            Console.WriteLine(error);
        }
    }
}
