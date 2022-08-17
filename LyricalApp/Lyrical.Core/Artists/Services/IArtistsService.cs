using Lyrical.Core.Artists.Messages.Queries;

namespace Lyrical.Core.Artists.Services
{
    public interface IArtistsService
    {
        Task<IEnumerable<string>> GetArtistsTracks(GetArtistsSongs query);
    }
}
