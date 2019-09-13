using MediatR;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details.View
{
	public class ViewArtistDetailsRequest : IRequest<ViewArtistDetailsResponse>
	{
		public int ArtistId { get; set; }
	}
}