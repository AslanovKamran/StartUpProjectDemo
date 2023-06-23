namespace StartUpProjectDemo.Models.Domain
{
	public class Product
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string Poster { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int TargetId { get; set; }
	}
}
