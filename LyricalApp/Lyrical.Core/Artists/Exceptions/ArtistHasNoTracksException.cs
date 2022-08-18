namespace Lyrical.Core.Artists.Exceptions
{
    public class ArtistHasNoTracksException : Exception
    {
        public ArtistHasNoTracksException(string artistName) : base($"No tracks found for {artistName}")
        {
        }
    }
}
