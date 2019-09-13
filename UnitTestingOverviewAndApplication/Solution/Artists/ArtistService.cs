using System.Threading.Tasks;

namespace UnitTestingOverviewAndApplication.Solution.Artists
{
	public class ArtistService : IArtistService
	{
		public Task<Artist> GetByArtist(int artistId)
		{
			return Task.FromResult<Artist>(null);
		}
	}
}