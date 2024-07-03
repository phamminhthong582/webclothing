using DataAccessLayer;
using DataAccessLayer.Repositorys;
using DataAccessLayer.Repositorys.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Request.Category;
using ModelLayer.DTO.Response.Category;
using ModelLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly WebCoustemClothingContext _context;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategorys()
        {
            var cate = await _categoryRepository.GetAllCategoryAsync();
            return Ok(cate);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryRespone>> UpdateCategory(int id , UpdateCategoryRequest model)
        {
            if (id != model.CategoryId)
            {
                return BadRequest("ID không khớp");
            }
            try
            {
                var updatecate = await _categoryRepository.UpdateCategory(model);
                return Ok(updatecate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<CategoryRespone>> CreateCategory(CreateCategoryRequest model)
        {
            try
            {
                var createcate = await _categoryRepository.CreateCategory(model);
                return Ok(createcate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryRespone>> GetCategoryById(int id)
        {
            var cate = await _categoryRepository.GetCategoryById(id);
            return Ok(cate);
        }
    }
}
