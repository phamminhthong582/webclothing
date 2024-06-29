using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class CategoryRepository  /*ICategoryRepository*/
    {
        private readonly WebCoustemClothingContext _context;
        public CategoryRepository(WebCoustemClothingContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateCategory(Category Categorys)
        {
            var cate = new Category
            {
                CategoryName = Categorys.CategoryName,
                CreateDay = DateTime.Now,
            };
            await _context.Categories.AddAsync(cate);
            await _context.SaveChangesAsync();
            return cate;
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

        public async Task<Category> GetCategoryById(int id)
        {
           var cate = await _context.Categories.FirstOrDefaultAsync(a => a.CategoryId == id);
            return cate;
        }

        //public async Task<Category> UpdateCategory(Category request)
        //{
        //    return await _context.Categories.ToListAsync();
        //}
    }
}
