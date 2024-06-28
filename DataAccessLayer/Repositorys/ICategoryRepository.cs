using ModelLayer.DTO.Response.Account;
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
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(Category userAccount);

        Task<Category> UpdateCategory(Category request);
        Task DeleteCategoryAsync(int id);
       
    }
}
