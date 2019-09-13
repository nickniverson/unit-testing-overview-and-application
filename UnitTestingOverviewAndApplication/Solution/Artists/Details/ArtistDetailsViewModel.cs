using System.Collections.Generic;
using System.Linq;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details
{
	public class ArtistDetailsViewModel
	{
		public int ArtistId { get; set; }

		public string ArtistName { get; set; }

		public int ArtistTotalAlbumCount => Albums?.Count ?? 0;

		public int ArtistTotalSongCount => Albums?.Sum(album => album.TotalSongCount) ?? 0;

		public List<AlbumViewModel> Albums { get; set; }

		public bool HasError => !string.IsNullOrWhiteSpace(FriendlyErrorMessage);

		public string FriendlyErrorMessage { get; set; }
	}
}