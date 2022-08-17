namespace Lyrical.Core.Lyrics.Messages.Queries
{
    public class GetAverageLyrics
    {
        public string ArtistName { get; set; }

        public IEnumerable<string> Tracks { get; set; }
    }
}
