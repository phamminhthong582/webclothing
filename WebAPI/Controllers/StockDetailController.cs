using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Repositorys;
using ModelLayer.DTO.Request.Stock;
using ModelLayer.DTO.Response.Stock;
using ModelLayer.DTO.Request.StockDetails;
using ModelLayer.DTO.Response.StockDetails;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockDetailController : ControllerBase
    {
        private readonly IStockDetailRepository _stockDetailRepository;

        public StockDetailController(IStockDetailRepository stockDetailRepository)
        {
            _stockDetailRepository = stockDetailRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDetailResponseDTO>>> GetAllStockDetails()
        {
            var stockDetails = await _stockDetailRepository.GetAllStockDetails();
            return Ok(stockDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockDetailResponseDTO>> GetStockDetailById(int id)
        {
            var stockDetail = await _stockDetailRepository.GetStockDetailById(id);
            if (stockDetail == null)
            {
                return NotFound();
            }
            return Ok(stockDetail);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateStockDetail([FromForm] CreateStockDetailRequest model)
        {
            try
            {
                var stockDetail = await _stockDetailRepository.CreateStockDetail(model);
                return Ok(stockDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateStockDetail(int id, [FromForm] UpdateStockDetailRequest model)
        {
            if (id != model.StockDetailId)
            {
                return BadRequest("ID không khớp");
            }

            try
            {
                var updatedStockDetail = await _stockDetailRepository.UpdateStockDetail(model);
                if (updatedStockDetail == null)
                {
                    return NotFound();
                }
                return Ok(updatedStockDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteStockDetailAsync(int id)
        {
            try
            {
                await _stockDetailRepository.DeleteStockDetailAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
