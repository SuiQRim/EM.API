using EM.API.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EMAPI.Tests
{
	internal class CoordinatesControllerTests { 

		[Test]
		public async Task GenerateCoordinates_Count2_ReturnOk()
		{
			//Arrange
			EMApplicationFactory application = new ();
			HttpClient httpClient = application.CreateClient();
			int count = 2;

			//Act
			HttpResponseMessage response = await httpClient.GetAsync($"/api/coordinates?count={count}");

			//Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

		}

		[Test]
		public async Task GenerateCoordinates_Count0_ReturnBadRequest()
		{
			//Arrange
			EMApplicationFactory application = new();
			HttpClient httpClient = application.CreateClient();
			int count = 0;

			//Act
			HttpResponseMessage response = await httpClient.GetAsync($"/api/coordinates?count={count}");

			//Assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

		}

		[Test]
		public async Task SumCoordinatesDistances_CoordinatesCount2_ReturnCompletedDistance()
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
			Assert.That(distance.Metres > 0, Is.True);

		}


		[Test]
		public async Task SumCoordinatesDistances_CoordinatesCount1_ReturnZeroDistance()
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
				}
			];

			//Act
			Distance? distance = await SumCoordinatesDistancesRequest(httpClient, coordinates);

			//Assert
			Assert.That(distance.Metres, Is.EqualTo(0));

		}

		[Test]
		public async Task SumCoordinatesDistances_Null_ReturnZeroDistance()
		{
			//Arrange
			EMApplicationFactory application = new();
			HttpClient httpClient = application.CreateClient();

			//Act
			Distance? distance = await SumCoordinatesDistancesRequest(httpClient, null);
			
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
