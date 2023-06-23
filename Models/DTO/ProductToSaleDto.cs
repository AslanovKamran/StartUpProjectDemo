using StartUpProjectDemo.Models.Domain;

namespace StartUpProjectDemo.Models.DTO
{
	public class ProductToSaleDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string Poster { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int TargetId { get; set; }
		public List<ProductOptions> ProductOptions { get; set; } = new();
	}
}
