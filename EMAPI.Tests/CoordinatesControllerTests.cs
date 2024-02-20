using EM.API.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EMAPI.Tests
{
	internal class CoordinatesControllerTests {

		[Test]
		[TestCase(2, HttpStatusCode.OK)]
		[TestCase(0, HttpStatusCode.BadRequest)]
		public async Task GenerateCoordinates_Count_ReturnCode(int value, HttpStatusCode expected)
		{
			//Arrange
			EMApplicationFactory application = new ();
			HttpClient httpClient = application.CreateClient();

			//Act
			HttpResponseMessage response = await httpClient.GetAsync($"/api/coordinates?count={value}");

			//Assert
			Assert.That(response.StatusCode, Is.EqualTo(expected));

		}


		[Test]
		public async Task SumCoordinatesDistances_2Coordinates_ReturnCompletedDistance()
		{
			//Arrange
			EMApplicationFactory application = new();
			HttpClient httpClient = application.CreateClient();
			Coordinate[] coordinates =
			[
				new ()
				{
					Latitude = 60.021158,
					Longitude = 30.321135
				},
				new ()
				{
					Latitude =  60.024157,
					Longitude = 30.323133
				}
			];

			//Act
			Distance? distance = await SumCoordinatesDistancesRequest(httpClient, coordinates);

			//Assert
			Assert.That(distance.Metres > 0 && distance.Miles > 0, Is.True);

		}


		private static readonly object[] _incompleteLists =
		[
			new object[] {
				new Coordinate[]
				{
					new ()
					{
						Latitude = 60,
						Longitude = 30
					}
				},
			},
			new object[] {
				Array.Empty<Coordinate>()
			},
			new object[] {
				null
			},

		];

		[Test]
		[TestCaseSource(nameof(_incompleteLists))]
		public async Task SumCoordinatesDistances_IncompleteList_ReturnZeroDistance(Coordinate[] coordinates)
		{
			//Arrange
			EMApplicationFactory application = new();
			HttpClient httpClient = application.CreateClient();

			//Act
			Distance? distance = await SumCoordinatesDistancesRequest(httpClient, coordinates);

			//Assert
			Assert.That(distance.Metres, Is.EqualTo(0));

		}

		private static async Task<Distance?> SumCoordinatesDistancesRequest(HttpClient httpClient, Coordinate[]? coordinates)
		{
			string json = JsonConvert.SerializeObject(coordinates);
			StringContent content = new (
							json,
							Encoding.UTF8,
							"application/json");

			HttpResponseMessage response = await httpClient.PostAsync($"/api/coordinates", content);

			Stream stream = await response.Content.ReadAsStreamAsync();
			using var reader = new StreamReader(stream);
			string data = reader.ReadToEnd();
			Distance? distance = JsonConvert.DeserializeObject<Distance>(data);
			return distance;
		}

	}
}
