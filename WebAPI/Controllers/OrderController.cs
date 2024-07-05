using DataAccessLayer.Repositorys;
using DataAccessLayer.Repositorys.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request.Order;
using ModelLayer.DTO.Response.Order;
using ModelLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrder()
        {
            var order = await _orderRepository.GetAllOrderAsync();
            return Ok(order);
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateOrder(int id, [FromForm] UpdateOrder model)
        {
            if (id != model.OrderId)
            {
                return BadRequest("ID không khớp");
            }
            try
            {
                var updatorder = await _orderRepository.UpdateOrder(model);
                return Ok(updatorder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> CreateOrder([FromForm] CreateOrder model)
        {
            try
            {
                var createorder = await _orderRepository.CreateOrder(model);
                return Ok(createorder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRespone>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            return Ok(order);
        }
        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteOrderÁsync(int id)
        {
            try
            {
                await _orderRepository.DeleteOrderAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<Order>> GetOrderByAccountId(int accountId)
        {
            var order = await _orderRepository.GetOrderByAccountId(accountId);
            return Ok(order);
        }
    }
}
