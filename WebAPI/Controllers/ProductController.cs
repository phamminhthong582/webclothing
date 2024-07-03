using DataAccessLayer.Repositorys;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using ModelLayer.DTO.Request.Products;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ModelLayer.DTO.Response.Products;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly WebCoustemClothingContext _context;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;        
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var pro = await _productRepository.GetAllProductAsync();
            return Ok(pro);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id , UpdateProductRequest model)
        {
            if (id != model.ProductId)
            {
                return BadRequest("ID không khớp");
            }
            try
            {
                var updatepro = await _productRepository.UpdateProduct(model);
                return Ok(updatepro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductRequest model)
        {
            try
            {
                var createpro = await _productRepository.CreateProduct(model);
                return Ok(createpro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRespone>> GetProductById(int id)
        {
            var pro = await _productRepository.GetProductById(id);
            return Ok(pro);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            try
            {
                await _productRepository.DeleteProductAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
