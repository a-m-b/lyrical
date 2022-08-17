using Lyrical.Core.Artists.Exceptions;
using Lyrical.Core.Artists.Messages.Queries;
using Lyrical.Core.Artists.Repositories;

namespace Lyrical.Core.Artists.Services
{
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistsRepository _artistsRepository;

        public ArtistsService(IArtistsRepository artistsRepository)
        {
            _artistsRepository = artistsRepository;
        }

        public async Task<IEnumerable<string>> GetArtistsTracks(GetArtistsSongs query)
        {
            if (string.IsNullOrEmpty(query.ArtistName))
            {
                throw new ArtistNotProvidedException();
            }

            var artistId = await _artistsRepository.GetFirstArtistId(query.ArtistName);

            if (string.IsNullOrEmpty(artistId))
            {
                throw new ArtistNotFoundException(query.ArtistName);
            }

            var albumIds = await _artistsRepository.GetAlbumIds(artistId);

            var artistSongs = new List<string>();

            foreach (var albumId in albumIds)
            {
                var trackNames = await _artistsRepository.GetTrackNames(albumId);

                artistSongs.AddRange(trackNames);
            }    

            return artistSongs;
        }
    }
}
