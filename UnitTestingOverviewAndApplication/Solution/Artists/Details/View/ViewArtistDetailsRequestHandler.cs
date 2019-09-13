using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UnitTestingOverviewAndApplication.Solution.Artists.Details.View
{
	public class ViewArtistDetailsRequestHandler : IRequestHandler<ViewArtistDetailsRequest, ViewArtistDetailsResponse>
	{
		private readonly IArtistService _artistService;
		private readonly IAlbumService _albumService;


		public ViewArtistDetailsRequestHandler(IArtistService artistService, IAlbumService albumService)
		{
			_artistService = artistService ?? throw new ArgumentNullException(nameof(artistService));
			_albumService = albumService ?? throw new ArgumentNullException(nameof(albumService));
		}


		public async Task<ViewArtistDetailsResponse> Handle(ViewArtistDetailsRequest request, CancellationToken cancellationToken)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			Artist artist = await _artistService.GetByArtist(request.ArtistId);

			if (artist == null)
				return new ViewArtistDetailsNotFoundResponse(request.ArtistId);

			IEnumerable<Album> albums = await _albumService.GetByArtist(request.ArtistId);

			return new ViewArtistDetailsResponse
			{
				ViewModel = new ArtistDetailsViewModel
				{
					ArtistId = artist.Id,
					ArtistName = artist.Name,
					Albums = MapToAlbumViewModelList(albums)
				}
			};
		}


		private List<AlbumViewModel> MapToAlbumViewModelList(IEnumerable<Album> albums)
		{
			if (albums == null)
			{
				return new List<AlbumViewModel>();
			}

			return albums
				.Where(album => album != null)
				.Select(album => new AlbumViewModel
					{
						Id = album.Id,
						Name = album.Name
					})
				.ToList();
		}
	}
}