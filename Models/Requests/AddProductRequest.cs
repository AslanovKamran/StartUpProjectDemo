using StartUpProjectDemo.Models.Domain;

namespace StartUpProjectDemo.Models.Requests
{
	public class AddProductRequest
	{
		public string Title { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string Poster { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int TargetId { get; set; }

	}
}
