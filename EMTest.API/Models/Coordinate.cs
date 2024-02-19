using System.ComponentModel.DataAnnotations;

namespace EMTest.API.Models
{
	public class Coordinate
	{
		[Range(-90,90)]
		public double Latitude { get; set; }
		[Range(-180, 180)]
		public double Longitude { get; set; }
	}
}
