using Lyrical.Core.Lyrics.Messages.Queries;

namespace Lyrical.Core.Lyrics.Services
{
    public interface ILyricsService
    {
        Task<int> GetTrackWordAverage(GetTrackLyricAverage query);
    }
}
