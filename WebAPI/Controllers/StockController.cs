using DataAccessLayer.Repositorys;
using DataAccessLayer.Repositorys.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request.Products;
using ModelLayer.DTO.Request.Stock;
using ModelLayer.DTO.Response.Products;
using ModelLayer.DTO.Response.Stock;
using ModelLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]


    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllStock()
        {
            var stock = await _stockRepository.GetAllStock();
            return Ok(stock);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<StockResponseDTO>> GetStockById(int id)
        {
            var stock = await _stockRepository.GetStockById(id);
            return Ok(stock);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> CreateStock([FromForm] CreateProductStockRequest model)
        {
            try
            {
                var stock = await _stockRepository.CreateStock(model);
                return Ok(stock);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateStock(int id, [FromForm] UpdateProductStockRequest model)
        {
            if (id != model.StockId)
            {
                return BadRequest("ID không khớp");
            }
            try
            {
                var updateStock = await _stockRepository.UpdateStock(model);
                return Ok(updateStock);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("remove/{id}")]
        public async Task<IActionResult> DeleteStockAsync(int id)
        {
            try
            {
                await _stockRepository.DeleteStockAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
