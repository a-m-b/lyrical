namespace Lyrical.Core.Artists.Repositories
{
    public interface IArtistsRepository
    {
        Task<string?> GetFirstArtistId(string artistName);

        Task<List<string>?> GetAlbumIds(string artistId);

        Task<List<string>?> GetTrackNames(string albumId);
    }
}
