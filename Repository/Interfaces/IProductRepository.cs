using StartUpProjectDemo.Models.Domain;
using StartUpProjectDemo.Models.DTO;
using StartUpProjectDemo.Models.Requests;

namespace StartUpProjectDemo.Repository.Interfaces
{
	public interface IProductRepository
	{
		Task<List<ProductToSaleDto>> GetAllProductsAsync();
		Task<ProductToSaleDto> GetProductByIdAsync(int id);
		Task<List<ProductOptions>> GetProductOptionsByIdAsync(int id);

		Task <ProductToSaleDto> AddProductAsync(AddProductRequest request);
		Task <ProductToSaleDto> AddProductOptionsByIdAsync(int id, int colorId, int sizeId, int amount);
		Task <ProductToSaleDto> UpdateProductAsync(Product product);

		Task DeleteProductByIdAsync(int id);
	}
}
