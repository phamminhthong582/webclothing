using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Response;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface IProductRepository
    {
        Task<List<ProductRespone>> GetAllProductAsync();
        Task<ProductRespone> GetProductById(int id);
        Task<Product> CreateProduct(ProductRespone product);
        Task<Product> UpdateProduct(ProductRespone product);
        Task DeleteProductAsync(int id);
    }
}
