namespace Lyrical.Core.Artists.Exceptions
{
    public class ArtistNotFoundException : Exception
    {
        public ArtistNotFoundException(string artistName) : base($"No artist matches {artistName}")
        {
        }
    }
}
