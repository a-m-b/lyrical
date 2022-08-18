using Lyrical.Core.Lyrics.Exceptions;
using Lyrical.Core.Lyrics.Messages.Queries;
using Lyrical.Core.Lyrics.Repositories;
using System.Threading.Tasks;

namespace Lyrical.Core.Lyrics.Services
{
    public class LyricsService : ILyricsService
    {
        private readonly ILyricsRepository _lyricsRepository;

        public LyricsService(ILyricsRepository lyricsRepository)
        {
            _lyricsRepository = lyricsRepository;
        }

        public async Task<int> GetTrackWordAverage(GetTrackLyricAverage query)
        {
            var trackWordCounts = new List<int>();

            var trackWordCountQuery = 
                from trackName in query.Tracks.Take(20)
                select GetTrackWordCount(query.ArtistName, trackName);

            var trackWordCountTasks = trackWordCountQuery.ToList();

            while (trackWordCountTasks.Any())
            {
                var finishedTask = await Task.WhenAny(trackWordCountTasks);
                trackWordCountTasks.Remove(finishedTask);
                var result = await finishedTask;
                if (result > 0)
                {
                    trackWordCounts.Add(result);
                }
            }

            return trackWordCounts.Any() ? trackWordCounts.Sum() / trackWordCounts.Count : 0;
        }

        private async Task<int> GetTrackWordCount(string artistName, string trackName)
        {
            var lyrics = await _lyricsRepository.GetLyrics(artistName, trackName);

            if (string.IsNullOrEmpty(lyrics))
            {
                Console.WriteLine($"Could not retrieved lyrics for track: {trackName}");
                return 0;
            }

            Console.WriteLine($"Retrieved lyrics for track: {trackName}");

            var wordCount = lyrics.Split(' ');

            return wordCount.Length;
        }
    }
}
