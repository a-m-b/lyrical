using Lyrical.Core.Lyrics.DTOs;
using Lyrical.Core.Lyrics.Repositories;
using Newtonsoft.Json;

namespace Lyrical.Infrastructure.LyricsOvh.Client
{
    public class LyricsClient : ILyricsRepository
    {
        private readonly HttpClient _lyricsClient;

        public LyricsClient(IHttpClientFactory httpClientFactory)
        {
            _lyricsClient =  httpClientFactory.CreateClient("Lyrics");
        }

        public async Task<string?> GetLyrics(string artistName, string trackName)
        {
            var response = await _lyricsClient.GetAsync($"{artistName}/{trackName}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            return (JsonConvert.DeserializeObject<dynamic>(content))?.lyrics;
        }
    }
}
