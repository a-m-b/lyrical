using Lyrical.Core.Artists.Exceptions;
using Lyrical.Core.Artists.Messages.Queries;
using Lyrical.Core.Artists.Services;
using Lyrical.Core.Lyrics.Exceptions;
using Lyrical.Core.Lyrics.Messages.Queries;
using Lyrical.Core.Lyrics.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                if (!trackNames.Any())
                {
                    SendMessage("No tracks found");
                }

                var averageWords = await _lyricsService.GetAverageLyrics(new GetAverageLyrics() { ArtistName = artistName, Tracks = trackNames });

                SendMessage($"The average word count is {averageWords}");
            }
            catch (ArtistNotProvidedException ex)
            {
                SendMessage(ex.Message);
            }
            catch (ArtistNotFoundException ex)
            {
                SendMessage(ex.Message);
            }
            catch (NoLyricsFoundException ex)
            {
                SendMessage(ex.Message);
            }
            catch (Exception ex)
            {
                SendMessage("There was an error!");
                SendMessage(ex.Message);
            }

            _host.StopAsync();
        }

        private void SendMessage(string text)
        {
            Console.WriteLine("####");
            Console.WriteLine($"#### {text}");
            Console.WriteLine("####");
        }
    }
}
