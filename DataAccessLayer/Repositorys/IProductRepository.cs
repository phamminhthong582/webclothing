using ModelLayer.DTO.Request.Account;
using ModelLayer.DTO.Request.Products;
using ModelLayer.DTO.Response;
using ModelLayer.DTO.Response.Account;
using ModelLayer.DTO.Response.Products;
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
        Task<ProductRespone> CreateProduct(CreateProductRequest request);
        Task<ProductRespone> UpdateProduct(UpdateProductRequest request);
        Task DeleteProductAsync(int id);
        public Task<List<ProductRespone>> FilterProducts(FilterProduct filter);
    }
}
