namespace Lyrical.Core.Lyrics.Messages.Queries
{
    public class GetTrackLyricAverage
    {
        public string ArtistName { get; set; }

        public IEnumerable<string> Tracks { get; set; }
    }
}
