using Dapper;
using StartUpProjectDemo.Models.Domain;
using StartUpProjectDemo.Models.DTO;
using StartUpProjectDemo.Models.Requests;
using StartUpProjectDemo.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace StartUpProjectDemo.Repository.Dapper
{
	public class ProductRepositoryDapper : IProductRepository
	{
		private readonly string _connectionString;
		public ProductRepositoryDapper(string connectionString) => _connectionString = connectionString;

		public async Task<List<ProductToSaleDto>> GetAllProductsAsync()
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{

				string query = @"exec GetAllProducts";
				var result = (await db.QueryAsync<Product>(query, null)).ToList();

				var response = new List<ProductToSaleDto>();

				for (int i = 0; i < result.Count; i++)
				{
					ProductToSaleDto prod = new()
					{
						Id = result[i].Id,
						Title = result[i].Title,
						Price = result[i].Price,
						Poster = result[i].Poster,
						Description = result[i].Description,
						TargetId = result[i].TargetId,
						ProductOptions = await GetProductOptionsByIdAsync(result[i].Id)
					};
					response.Add(prod);
				}
				return response;
			}
		}

		public async Task<ProductToSaleDto> GetProductByIdAsync(int id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

				string query = @"exec GetProductById @Id ";
				var product = await db.QueryFirstOrDefaultAsync<Product>(query, parameters);
				if (product == null) return null!;
				var productOptions = await GetProductOptionsByIdAsync(product.Id);
				return new ProductToSaleDto 
				{
					Id = product.Id,
					Title = product.Title,
					Price = product.Price,
					Poster = product.Poster,
					Description = product.Description,
					TargetId = product.TargetId,
					ProductOptions = productOptions
				};
			}
		}

		public async Task<List<ProductOptions>> GetProductOptionsByIdAsync(int id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

				string query = @"exec GetProductOptionsById @Id ";
				var result = (await db.QueryAsync<ProductOptions>(query, parameters)).ToList();
				return result!;
			}
		}

		public async Task<ProductToSaleDto> AddProductAsync(AddProductRequest request)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("Title", request.Title, DbType.String, ParameterDirection.Input);
				parameters.Add("Price", request.Price, DbType.Decimal, ParameterDirection.Input);
				parameters.Add("Poster", request.Poster, DbType.String, ParameterDirection.Input);
				parameters.Add("Description", request.Description, DbType.String, ParameterDirection.Input);
				parameters.Add("TargetId", request.TargetId, DbType.Int32, ParameterDirection.Input);

				string query = @"INSERT INTO Products(Title, Price, Poster, Description, TargetId)
								OUTPUT INSERTED.Id
								VALUES (@Title, @Price, @Poster, @Description, @TargetId)";
				var insertedProductId = await db.QuerySingleAsync<int>(query, parameters);

				return (await GetProductByIdAsync(insertedProductId));

			}
		}

		public async Task<ProductToSaleDto> AddProductOptionsByIdAsync(int id, int colorId, int sizeId, int amount)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("ProductId", id, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ColorId", colorId, DbType.Int32, ParameterDirection.Input);
				parameters.Add("SizeId", sizeId, DbType.Int32, ParameterDirection.Input);
				parameters.Add("Amount", amount, DbType.Int32, ParameterDirection.Input);

				string query = @"INSERT INTO ProductsToSale VALUES (@ProductId,@ColorId,@SizeId,@Amount )";
				await db.ExecuteAsync(query, parameters);

				return (await GetProductByIdAsync(id));

			}
		}

		public async Task<ProductToSaleDto> UpdateProductAsync(Product product)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				string query = @"UpdateProduct @Id, @Title, @Price, @Poster, @Description, @TargetId";
				await db.ExecuteAsync(query, product);
				return (await GetProductByIdAsync(product.Id));

			}

		}

		public async Task DeleteProductByIdAsync(int id)
		{
		

			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

				string query = @"DELETE FROM Products WHERE Products.Id= @id ";
				await db.ExecuteAsync(query, parameters);
			}
		}

		//Delete Instantly

		private async Task DeleteFromProductsToSale(int id) 
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var parameters = new DynamicParameters();
				parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

				string query = @"DELETE FROM ProductsToSale WHERE Products.Id= @id";
				await db.ExecuteAsync(query, parameters);
			}
		}
	}
}
