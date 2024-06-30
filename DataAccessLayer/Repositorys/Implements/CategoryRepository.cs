
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Request.Category;
using ModelLayer.DTO.Response.Account;
using ModelLayer.DTO.Response.Category;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebCoustemClothingContext _context;
        public CategoryRepository(WebCoustemClothingContext context)
        {
            _context = context;
        }
        public async Task<CategoryRespone> CreateCategory(CreateCategoryRequest Categorys)
        {
            var cate = new Category
            {
                CategoryName = Categorys.CategoryName,
                CreateDay = DateTime.Now,
            };
            await _context.Categories.AddAsync(cate);
            await _context.SaveChangesAsync();
            var respone = new CategoryRespone
            {
                CategoryId = cate.CategoryId,
                CategoryName = cate.CategoryName,
                CreateDay = DateTime.Now,
            };
            return respone;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var cate = await _context.Categories.FindAsync(id);
            if (cate != null)
            {
                _context.Categories.Remove(cate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<CategoryRespone> GetCategoryById(int id)
        {
           var cate = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryId == id);
            if (cate != null)
            {
                return new CategoryRespone
                {
                   CategoryId= cate.CategoryId,
                   CategoryName= cate.CategoryName,
                   CreateDay = cate.CreateDay,
                };
            }
            return null;
        }

        public async Task<CategoryRespone> UpdateCategory(UpdateCategoryRequest request)
        {
            var cate = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryId == request.CategoryId);
            if (cate != null)
            {
                cate.CategoryName = request.CategoryName;
                cate.CreateDay = request.CreateDay;
            }
            await _context.SaveChangesAsync();
            var respone = new CategoryRespone
            {
                CategoryId = cate.CategoryId,
                CategoryName = cate.CategoryName,
                CreateDay = DateTime.Now,
            };
            return respone;
        }
    }
}
