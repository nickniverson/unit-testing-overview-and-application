using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details.Edit
{
	public class EditArtistDetailsRequestHandler : IRequestHandler<EditArtistDetailsRequest, EditArtistDetailsResponse>
	{
		public Task<EditArtistDetailsResponse> Handle(EditArtistDetailsRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}