namespace UnitTestingOverviewAndApplication.Solution.Artists.Details.Edit
{
	public class EditArtistDetailsNotFoundResponse: EditArtistDetailsResponse
	{
		public EditArtistDetailsNotFoundResponse(int artistId)
		{
			ViewModel = new ArtistDetailsNotFoundViewModel(artistId);
		}
	}
}