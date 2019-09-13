using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnitTestingOverviewAndApplication.Exercises.Artists.Details.View;

namespace UnitTestingOverviewAndApplication.Exercises.Artists.Details
{
	[Route("api/exercises/[controller]")]
	[ApiController]
	public class ArtistDetailsController : ControllerBase
	{
		private readonly IMediator _mediator;


		public ArtistDetailsController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}


		[HttpGet("")]
		public async Task<IActionResult> Get([FromQuery] ViewArtistDetailsRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
	}
}