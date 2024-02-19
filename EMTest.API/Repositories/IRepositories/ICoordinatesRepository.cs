using EMTest.API.Models;

namespace EMTest.API.Repositories.IRepositories
{
	public interface ICoordinatesRepository
	{
		public IEnumerable<Coordinate> Generate();

		public Distance SumCoordinatesDistance(List<Coordinate> coordinates);

	}
}
