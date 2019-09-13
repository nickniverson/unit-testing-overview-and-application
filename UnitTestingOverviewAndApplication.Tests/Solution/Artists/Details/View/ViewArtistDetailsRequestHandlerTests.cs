using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTestingOverviewAndApplication.Solution.Artists;
using UnitTestingOverviewAndApplication.Solution.Artists.Details.View;

namespace UnitTestingOverviewAndApplication.Tests.Solution.Artists.Details.View
{
	[TestCategory(TestCategories.Artists)]
	[TestCategory(TestCategories.ViewArtistDetails)]
	[TestClass]
	public class ViewArtistDetailsRequestHandlerTests
	{
		internal IRequestHandler<ViewArtistDetailsRequest, ViewArtistDetailsResponse> Target { get; set; }

		internal ViewArtistDetailsRequestHandlerBuilder Builder { get; set; }


		[TestInitialize]
		public virtual void TestInitialize()
		{
			Builder = new ViewArtistDetailsRequestHandlerBuilder();
		}



		[TestClass]
		public class Constructor : ViewArtistDetailsRequestHandlerTests
		{
			[TestMethod]
			public void Should_Create_An_Instance()
			{
				Target = Builder.Build();

				Assert.IsInstanceOfType(
					value: Target, 
					expectedType: typeof(ViewArtistDetailsRequestHandler));
			}


			[ExpectedException(typeof(ArgumentNullException))]
			[TestMethod]
			public void Should_Throw_An_Argument_Null_Exception_When_Given_A_Null_Artist_Service()
			{
				Builder.ArtistService = null;

				Target = Builder.Build();

				Fail();
			}


			[ExpectedException(typeof(ArgumentNullException))]
			[TestMethod]
			public void Should_Throw_An_Argument_Null_Exception_When_Given_A_Null_Album_Service()
			{
				Builder.AlbumService = null;

				Target = Builder.Build();

				Fail();
			}


			private void Fail()
			{
				Assert.Fail($"expected {nameof(ArgumentNullException)} to be thrown but wasn't");
			}
		}



		[TestClass]
		public class HandleMethod : ViewArtistDetailsRequestHandlerTests
		{
			public ViewArtistDetailsRequest Request { get; set; }

			public CancellationToken CancellationToken { get; set; }

			public ViewArtistDetailsResponse Response { get; set; }

			public Artist Artist { get; set; }

			public Album Album { get; set; }

			public List<Album> Albums { get; set; }


			[TestInitialize]
			public override void TestInitialize()
			{
				base.TestInitialize();

				Artist = new Artist
				{
					Id = 1,
					Name = "311"
				};

				Album = new Album
				{
					Id = 1,
					Name = "Voyager",
					ArtistId = Artist.Id
				};

				Albums = new List<Album>
				{
					Album
				};

				Request = new ViewArtistDetailsRequest
				{
					ArtistId = Artist.Id
				};

				CancellationToken = new CancellationToken();

				Target = Builder
					.SetupArtistServiceGetByArtistToReturn(Artist)
					.SetupAlbumServiceGetByArtistToReturn(Artist.Id, Albums)
					.Build();
			}


			[TestMethod]
			public async Task Should_Return_The_Artist_Details_Response()
			{
				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.IsNotNull(Response);
			}


			[TestMethod]
			public async Task Should_Populate_View_Model_On_Artist_Details_Response()
			{
				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.IsNotNull(Response.ViewModel);
			}


			[TestMethod]
			public async Task Should_Return_Artist_Name()
			{
				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.IsFalse(
					condition: string.IsNullOrWhiteSpace(Response.ViewModel.ArtistName),
					message: "artist name is either null, empty, or contains only whitespace");
			}


			[TestMethod]
			public async Task Should_Return_Total_Album_Count()
			{
				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.AreEqual(
					expected: 1, 
					actual: Response.ViewModel.ArtistTotalAlbumCount);
			}


			[TestMethod]
			public async Task Should_Return_Zero_For_Artist_Total_Album_Count_When_Album_Collection_Is_Null()
			{
				Builder.SetupAlbumServiceToReturnNull();

				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.AreEqual(
					expected: 0,
					actual: Response.ViewModel.ArtistTotalAlbumCount);
			}


			[TestMethod]
			public async Task Should_Set_View_Model_Has_Error_To_True_When_Artist_Not_Found()
			{
				Builder.SetupArtistServiceToReturnNull();

				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.IsTrue(
					condition: Response.ViewModel.HasError,
					message: "Expected Response.ViewModel.HasError to be true when Artist is not found");
			}


			[TestMethod]
			public async Task Should_Set_View_Model_Artist_Name_To_Not_Found_When_Artist_Not_Found()
			{
				Builder.SetupArtistServiceToReturnNull();

				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.AreEqual(
					expected: "Not Found",
					actual: Response.ViewModel.ArtistName);
			}


			[TestMethod]
			public async Task Should_Set_View_Model_Friendly_Error_Message_When_Artist_Not_Found()
			{
				Builder.SetupArtistServiceToReturnNull();

				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.IsFalse(string.IsNullOrWhiteSpace(Response.ViewModel.FriendlyErrorMessage));
			}


			[TestMethod]
			public async Task Should_Include_Artist_Id_In_Friendly_Error_Message_When_Artist_Not_Found()
			{
				Builder.SetupArtistServiceToReturnNull();

				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.IsTrue(Response.ViewModel.FriendlyErrorMessage.Contains(Request.ArtistId.ToString()));
			}


			[TestMethod]
			public async Task Should_Return_Empty_Album_List_When_Artist_Not_Found()
			{
				Builder.SetupArtistServiceToReturnNull();

				Response = await Target.Handle(
					Request,
					CancellationToken);

				Assert.IsNotNull(
					value: Response.ViewModel.Albums, 
					message: "expected albums list to be empty but was null");

				Assert.IsTrue(
					condition: Response.ViewModel.Albums.Count == 0, 
					message: "expected albums list to be empty");
			}
		}



		internal class ViewArtistDetailsRequestHandlerBuilder
		{
			// Artist Service
			internal IArtistService ArtistService { get; set; }

			private Mock<IArtistService> MockArtistService { get; set; }

			// Album Service
			internal IAlbumService AlbumService { get; set; }

			private Mock<IAlbumService> MockAlbumService { get; set; }


			internal ViewArtistDetailsRequestHandlerBuilder()
			{
				MockArtistService = new Mock<IArtistService>();
				ArtistService = MockArtistService.Object;

				MockAlbumService = new Mock<IAlbumService>();
				AlbumService = MockAlbumService.Object;
			}


			internal IRequestHandler<ViewArtistDetailsRequest, ViewArtistDetailsResponse> Build()
			{
				return new ViewArtistDetailsRequestHandler(ArtistService, AlbumService);
			}


			public ViewArtistDetailsRequestHandlerBuilder SetupArtistServiceGetByArtistToReturn(Artist artist)
			{
				MockArtistService
					.Setup(service => service.GetByArtist(It.Is<int>(id => id == artist.Id)))
					.Returns(Task.FromResult(artist));

				return this;
			}


			public ViewArtistDetailsRequestHandlerBuilder SetupAlbumServiceGetByArtistToReturn(int artistId, IEnumerable<Album> albums)
			{
				MockAlbumService
					.Setup(service => service.GetByArtist(It.Is<int>(id => id == artistId)))
					.Returns(Task.FromResult(albums));

				return this;
			}


			public ViewArtistDetailsRequestHandlerBuilder SetupAlbumServiceToReturnNull()
			{
				MockAlbumService
					.Setup(service => service.GetByArtist(It.IsAny<int>()))
					.Returns(Task.FromResult<IEnumerable<Album>>(null));

				return this;
			}


			public ViewArtistDetailsRequestHandlerBuilder SetupArtistServiceToReturnNull()
			{
				MockArtistService
					.Setup(service => service.GetByArtist(It.IsAny<int>()))
					.Returns(Task.FromResult<Artist>(null));

				return this;
			}
		}
	}
}
