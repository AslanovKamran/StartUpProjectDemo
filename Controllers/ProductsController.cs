using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartUpProjectDemo.Extensions;
using StartUpProjectDemo.Models.Domain;
using StartUpProjectDemo.Models.Requests;
using StartUpProjectDemo.Repository.Interfaces;

namespace StartUpProjectDemo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductRepository _repos;
		public ProductsController(IProductRepository repos) => _repos = repos;

		/// <summary>
		/// Gets a List of Products their Options
		/// </summary>
		/// <returns>ProductToSaleDto List</returns>

		[HttpGet]
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> GetAll(int? gender, string? sortBy = "title", string? orderBy = "asc")
		{
			var response = await _repos.GetAllProductsAsync();
			return response != null
				? Ok(response.Gender(gender!).Sort(sortBy!, orderBy!))
				: NotFound(new ProblemDetails { Status = 404, Title = "No Products" });
		}

		/// <summary>
		/// Gets a Single Product with its Options
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ProductToSaleDto</returns>
		
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		[Authorize(Roles = "User,Admin")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var response = await _repos.GetProductByIdAsync(id);
			return response != null
				? Ok(response)
				: NotFound(new ProblemDetails { Status = 404, Title = "Wrong Id or no such product" });
		}


		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Add([FromForm] AddProductRequest request) 
		{
			var res  = await _repos.AddProductAsync(request);
			return Ok(res);

		}

		[HttpPost]
		[Route("addOptions")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddOptions(int productId, int colorId,int sizeId, int amount)
		{
			var res = await _repos.AddProductOptionsByIdAsync(productId, colorId, sizeId, amount);
			return Ok(res);

		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateProduct(Product product) 
		{
			var res = await _repos.UpdateProductAsync(product);
			return Ok(res);
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteProduct(int id) 
		{
			await _repos.DeleteProductByIdAsync(id);
			return Ok("Deleted");
		}
	}



}
