using System.Collections.Generic;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details.View
{
	public class ViewArtistDetailsNotFoundResponse: ViewArtistDetailsResponse
	{
		public ViewArtistDetailsNotFoundResponse(int artistId)
		{
			ViewModel = new ArtistDetailsNotFoundViewModel(artistId);
		}
	}
}