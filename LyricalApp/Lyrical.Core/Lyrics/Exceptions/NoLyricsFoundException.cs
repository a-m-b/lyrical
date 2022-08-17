namespace Lyrical.Core.Lyrics.Exceptions
{
    public class NoLyricsFoundException : Exception
    {
        public NoLyricsFoundException(string artistName) : base($"No lyrics found for {artistName} tracks")
        {
        }
    }
}
