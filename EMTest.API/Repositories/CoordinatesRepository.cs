using EMTest.API.Models;
using EMTest.API.Repositories.IRepositories;
using System;

namespace EMTest.API.Repositories
{
	public class CoordinatesRepository : ICoordinatesRepository
	{
		public IEnumerable<Coordinate> Generate(int count)
		{
			Random rnd = new ();
			List<Coordinate> coordinates = new (count);

			for (int i = 0; i < count; i++)
			{
				coordinates.Add(new Coordinate
				{
					Latitude = rnd.NextDouble() * 180 - 90,
					Longitude = rnd.NextDouble() * 360 - 180
				});
			}

			return coordinates;
		}

		public Distance SumCoordinatesDistance(List<Coordinate> coordinates)
		{
			double sum = 0;

			if (coordinates.Count < 2)
			{
				return new Distance { Metres = 0, Miles = 0};
			}

			for (int i = 0; i + 1 < coordinates.Count; i++)
			{
				sum += GaversinusDistance(coordinates[i], coordinates[i + 1]);
			}	

			return new Distance { Metres = sum, Miles = sum * 0.00062137 };
		}

		private static double GaversinusDistance(Coordinate coordinate1, Coordinate coordinate2)
		{
			const double radius = 6371000;

			double lat = (coordinate2.Latitude - coordinate1.Latitude) * (Math.PI / 180);
			double lon = (coordinate2.Longitude - coordinate1.Longitude) * (Math.PI / 180);

			double rez = Math.Sin(lat / 2) * Math.Sin(lat / 2) + Math.Cos(coordinate1.Latitude * (Math.PI / 180)) *
				Math.Cos(coordinate2.Latitude * (Math.PI / 180)) * Math.Sin(lon / 2) * Math.Sin(lon / 2);

			double distance = 2 * radius * Math.Atan2(Math.Sqrt(rez), Math.Sqrt(1 - rez));
			return distance;
		}
	}
}
