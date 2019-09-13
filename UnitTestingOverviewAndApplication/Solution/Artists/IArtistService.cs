using System.Threading.Tasks;

namespace UnitTestingOverviewAndApplication.Solution.Artists
{
	public interface IArtistService
	{
		Task<Artist> GetByArtist(int artistId);
	}
}