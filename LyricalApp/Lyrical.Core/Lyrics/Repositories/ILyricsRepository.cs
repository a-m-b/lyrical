namespace Lyrical.Core.Lyrics.Repositories
{
    public interface ILyricsRepository
    {
        Task<string?> GetLyrics(string artistName, string trackName);
    }
}
