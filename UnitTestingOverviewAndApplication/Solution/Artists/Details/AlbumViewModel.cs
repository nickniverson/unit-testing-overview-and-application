using System.Collections.Generic;
using System.Linq;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details
{
	public class AlbumViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string AlbumArtImageUrl { get; set; }

		public List<SongViewModel> Songs { get; set; } = new List<SongViewModel>();

		public int TotalSongCount => Songs?.Count ?? 0;

		public int TotalAlbumLengthInMinutes => Songs?.Sum(song => song.Length.Minutes) ?? 0;
	}
}