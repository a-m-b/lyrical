namespace Lyrical.Core.Artists.Exceptions
{
    public class ArtistNotProvidedException : Exception
    {
        public ArtistNotProvidedException() : base("No artist provided")
        {
        }
    }
}
