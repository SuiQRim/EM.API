using EMTest.API.Models;
using EMTest.API.Repositories;
using EMTest.API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EMTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoordinatesController : ControllerBase
	{
		private readonly ICoordinatesRepository _coordinatesRepository;

        public CoordinatesController(ICoordinatesRepository coordinatesRepository)
        {
			_coordinatesRepository = coordinatesRepository;
		}

        [HttpGet]
		public ActionResult<IEnumerable<Coordinate>> Generate([Required] int count)
		{
			return Ok(_coordinatesRepository.Generate(count));
		}

		[HttpPost]
		public ActionResult<Distance> Distance(List<Coordinate>? coordinates)
		{
			return Ok(_coordinatesRepository.SumCoordinatesDistance(coordinates));
		}
	}
}
