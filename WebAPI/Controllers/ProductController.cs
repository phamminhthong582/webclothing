using DataAccessLayer.Repositorys;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using ModelLayer.DTO.Request.Products;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ModelLayer.DTO.Response.Products;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;       
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
        
        [HttpPut("update")]
        public async Task<ActionResult> UpdateProduct(int id , [FromForm] UpdateProductRequest model)
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
        
        [HttpPost("create")]      
        public async Task<ActionResult> CreateProduct([FromForm] CreateProductRequest model)
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
        
        [HttpDelete("remove/{id}")]
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
