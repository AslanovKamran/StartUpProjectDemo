using StartUpProjectDemo.Models.DTO;

namespace StartUpProjectDemo.Extensions
{
	public static class ProductsExtension
	{
		public static List<ProductToSaleDto> Sort(this List<ProductToSaleDto> products, string sortBy, string orderBy)
		{
			var filteredData = new List<ProductToSaleDto>();
			if (orderBy == "asc")
			{
				switch (sortBy)
				{
					case "price": { filteredData = products.OrderBy(p => p.Price).ToList(); } break;
					default:
						{ filteredData = products.OrderBy(p => p.Title).ToList(); }
						break;
				}
			}

			else if (orderBy == "desc")
			{
				switch (sortBy)
				{

					case "price": { filteredData = products.OrderByDescending(p => p.Price).ToList(); } break;
					default:
						{ filteredData = products.OrderByDescending(p => p.Title).ToList(); }
						break;
				}
			}

			else
			{
				return products.OrderBy(p => p.Title).ToList();
			}

			return filteredData;

		}

		public static List<ProductToSaleDto> Gender(this List<ProductToSaleDto> products, int? targetId)
		{
			if (targetId is null)  return products;

 			var filteredData = products.Where(x => x.TargetId == targetId).ToList();
			return filteredData == null ? products : filteredData;
		}
	}
}
