using Lyrical.Core.Lyrics.Exceptions;
using Lyrical.Core.Lyrics.Messages.Queries;
using Lyrical.Core.Lyrics.Repositories;

namespace Lyrical.Core.Lyrics.Services
{
    public class LyricsService : ILyricsService
    {
        private readonly ILyricsRepository _lyricsRepository;

        public LyricsService(ILyricsRepository lyricsRepository)
        {
            _lyricsRepository = lyricsRepository;
        }

        public async Task<int> GetAverageLyrics(GetAverageLyrics query)
        {
            var trackWordCounts = new List<int>();

            foreach (var trackName in query.Tracks.Take(20))
            {
                var lyrics = await _lyricsRepository.GetLyrics(query.ArtistName, trackName);

                if (string.IsNullOrEmpty(lyrics))
                {
                    continue;
                }

                var wordCount = lyrics.Split(' ');

                trackWordCounts.Add(wordCount.Length);
            }

            if (!trackWordCounts.Any())
            {
                throw new NoLyricsFoundException(query.ArtistName);
            }

            return trackWordCounts.Sum() / trackWordCounts.Count();
        }
    }
}
