using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTestingOverviewAndApplication.Solution.Artists
{
	public interface IAlbumService
	{
		Task<IEnumerable<Album>> GetByArtist(int artistId);
	}
}