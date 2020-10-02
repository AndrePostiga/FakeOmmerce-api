namespace FakeOmmerce.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class ProductDTO
	{
		[Required]
		public string Name { get; set; }
		
		[Required]
		public double Price { get; set; }

		[Required]
		public string Brand { get; set; }

		public string Description { get; set; }

		[Required]
		public List<string> Categories { get; set; }

		public List<string> Images { get; set; }

	}
}