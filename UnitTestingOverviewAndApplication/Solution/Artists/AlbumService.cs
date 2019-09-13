using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTestingOverviewAndApplication.Solution.Artists
{
	public class AlbumService : IAlbumService
	{
		public Task<IEnumerable<Album>> GetByArtist(int artistId)
		{
			throw new NotImplementedException();
		}
	}
}