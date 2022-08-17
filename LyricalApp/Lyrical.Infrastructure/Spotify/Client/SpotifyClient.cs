using Lyrical.Core.Artists.Repositories;
using Lyrical.Infrastructure.Spotify.Configuration;
using Microsoft.Extensions.Options;
using SpotifyAPI.Web;
using Spot = SpotifyAPI.Web.SpotifyClient;

namespace Lyrical.Infrastructure.Spotify.Client
{
    public class SpotifyClient : IArtistsRepository
    {
        private readonly Spot _spotifyClient;

        public SpotifyClient(IOptions<SpotifyConfiguration> options)
        {
            var config = SpotifyClientConfig
              .CreateDefault()
              .WithAuthenticator(new ClientCredentialsAuthenticator(options.Value.ClientId, options.Value.ClientSecret));
            _spotifyClient = new Spot(config);
        }

        public async Task<string?> GetFirstArtistId(string artistName)
        {
            var results = await _spotifyClient.Search.Item(new SearchRequest(SearchRequest.Types.Artist, artistName));

            return results?.Artists?.Items?.FirstOrDefault()?.Id;
        }

        public async Task<List<string>?> GetAlbumIds(string artistId)
        {
            var results = await _spotifyClient.Artists.GetAlbums(artistId);

            return results?.Items?.Select(a => a.Id).ToList();
        }

        public async Task<List<string>?> GetTrackNames(string albumId)
        {
            var results = await _spotifyClient.Albums.GetTracks(albumId);

            return results?.Items?.Select(t => t.Name).ToList();
        }
    }
}
