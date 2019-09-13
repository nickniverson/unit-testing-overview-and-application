using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UnitTestingOverviewAndApplication.Exercises.Artists.Details.View
{
	public class ViewArtistDetailsRequestHandler : IRequestHandler<ViewArtistDetailsRequest, ViewArtistDetailsResponse>
	{
		public Task<ViewArtistDetailsResponse> Handle(ViewArtistDetailsRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}