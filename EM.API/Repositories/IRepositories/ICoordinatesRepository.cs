using EM.API.Models;

namespace EM.API.Repositories.IRepositories
{
	public interface ICoordinatesRepository
	{
		public IEnumerable<Coordinate> Generate(int count);

		public Distance SumCoordinatesDistance(List<Coordinate>? coordinates);

	}
}
