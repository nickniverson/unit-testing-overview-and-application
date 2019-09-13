using MediatR;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details.Edit
{
	public class EditArtistDetailsRequest : IRequest<EditArtistDetailsResponse>
	{
		public ArtistDetailsViewModel ViewModel { get; set; }
	}
}