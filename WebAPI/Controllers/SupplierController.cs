using DataAccessLayer.Repositorys;
using DataAccessLayer.Repositorys.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Request.Supplier;
using ModelLayer.DTO.Response.Account;
using ModelLayer.DTO.Response.Supplier;
using ModelLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliers()
        {
            var sup = await _supplierRepository.GetAllSupplierAsync();
            return Ok(sup);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierRespone>> GetSupplierById(int id)
        {
            var sup = await _supplierRepository.GetSupplierById(id);
            return Ok(sup);
        }
        [HttpPost]
        public async Task<ActionResult<SupplierRespone>> CreateSupplier(CreateSupplierRequest model)
        {
            try
            {
                var createdSupplier = await _supplierRepository.CreateSupplier(model);
                return Ok(createdSupplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierRespone>> UpdateSupplier(int id, UpdateSupplierRequest model)
        {
            if (id != model.SupplierId)
            {
                return BadRequest("ID không khớp");
            }
            try
            {
                var updateSupplier = await _supplierRepository.UpdateSupplier(model);
                return Ok(updateSupplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierAsync(int id)
        {
            try
            {
                await _supplierRepository.DeleteSupplierAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
