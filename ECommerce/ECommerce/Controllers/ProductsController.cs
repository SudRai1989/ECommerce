using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand,string? type, string? sort)
        {
            return Ok(await repo.GetProductsAsync(brand, type, sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.AddProduct(product);

            if (await repo.SaveChangesAsync())
            {
                // GetProducts is upper method here
                return CreatedAtAction("GetProducts", new { id = product.Id }, product);
            }

            return BadRequest("Problem Creating Product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExist(id))
            {
                return BadRequest("Cannot Update This Product");
            }

            repo.UpdateProduct(product);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem Updating The Product!");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            repo.DeleteProduct(product);


            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem Deleting The Product!");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repo.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repo.GetTypesAsync());
        }
        private bool ProductExist(int id)
        {
            return repo.ProductExist(id);
        }
    }
}
