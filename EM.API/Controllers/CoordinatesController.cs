using EM.API.Models;
using EM.API.Repositories;
using EM.API.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EM.API.Controllers
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
			if (count < 1) 
			{
				return BadRequest();
			}

			return Ok(_coordinatesRepository.Generate(count));
		}

		[HttpPost]
		public ActionResult<Distance> Distance(List<Coordinate>? coordinates)
		{
			return Ok(_coordinatesRepository.SumCoordinatesDistance(coordinates));
		}
	}
}
