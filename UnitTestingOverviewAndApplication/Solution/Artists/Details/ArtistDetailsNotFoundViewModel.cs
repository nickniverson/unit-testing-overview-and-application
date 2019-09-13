using System.Collections.Generic;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details
{
	public class ArtistDetailsNotFoundViewModel : ArtistDetailsViewModel
	{
		public ArtistDetailsNotFoundViewModel(int artistId)
		{
			FriendlyErrorMessage = $"Sorry bro, but we can't find an artist with Id '{artistId}'.";
			ArtistName = "Not Found";
			Albums = new List<AlbumViewModel>();
		}
	}
}