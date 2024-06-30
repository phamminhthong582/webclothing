using ModelLayer.DTO.Request.Category;
using ModelLayer.DTO.Response.Account;
using ModelLayer.DTO.Response.Category;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync();
        Task<CategoryRespone> GetCategoryById(int id);
        Task<CategoryRespone> CreateCategory(CreateCategoryRequest request);
        Task<CategoryRespone> UpdateCategory(UpdateCategoryRequest request);      
        Task DeleteCategoryAsync(int id);
       
    }
}
