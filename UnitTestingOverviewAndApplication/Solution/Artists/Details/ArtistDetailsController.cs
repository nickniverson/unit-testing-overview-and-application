using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnitTestingOverviewAndApplication.Solution.Artists.Details.Edit;
using UnitTestingOverviewAndApplication.Solution.Artists.Details.View;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details
{
	[Route("api/solution/[controller]")]
	[ApiController]
	public class ArtistDetailsController : ControllerBase
	{
		private readonly IMediator _mediator;


		public ArtistDetailsController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}


		[HttpGet]
		public async Task<IActionResult> View([FromQuery]ViewArtistDetailsRequest request)
		{
			if (request == null)
				return BadRequest($"{nameof(request)} must not be null");

			if (request.ArtistId <= 0)
				return BadRequest($"{nameof(request.ArtistId)} must be greater than zero");

			return Ok(await _mediator.Send(request));
		}


		[HttpPost]
		public async Task<IActionResult> Edit([FromQuery]EditArtistDetailsRequest request)
		{
			if (request == null)
				return BadRequest($"{nameof(request)} must not be null");

			if (request.ViewModel == null)
				return BadRequest($"{nameof(request.ViewModel)} must not be null");

			return Ok(await _mediator.Send(request));
		}
	}
}
