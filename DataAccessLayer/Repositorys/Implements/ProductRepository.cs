using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Response;
using ModelLayer.DTO.Response.Account;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebCoustemClothingContext _context;
        public ProductRepository(WebCoustemClothingContext context)
        {
            _context = context;
        }
        public async Task<List<ProductRespone>> GetAllProductAsync()
        {
            var pro = _context.Products.Select(a => new ProductRespone
            {
                ProductId = a.ProductId,
                ProductName = a.ProductName,
                Price = a.Price,
                Quantity = a.Quantity,
                Image = a.Image,
                Color = a.Color,
                Size = a.Size,
                Desciption = a.Desciption,
                CreateDay = a.CreateDay,
                CategoryId = a.CategoryId,
            });        
            return pro.ToList();
        }
        public async Task<ProductRespone> GetProductById(int id)
        {
            var pro = await _context.Products.FirstOrDefaultAsync(a => a.ProductId == id);
            if (pro != null)
            {
                return new ProductRespone
                {
                    ProductId = pro.ProductId,
                    ProductName = pro.ProductName,
                    Price = pro.Price,
                    Quantity = pro.Quantity,
                    Image = pro.Image,
                    Color = pro.Color,
                    Size = pro.Size,
                    Desciption = pro.Desciption,
                    CreateDay = pro.CreateDay,
                    CategoryId = pro.CategoryId,
                };
            }
            return null;
        }
        public Task<Product> CreateProduct(ProductRespone product)
        {
            throw new NotImplementedException();
        }
        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Product> UpdateProduct(ProductRespone product)
        {
            throw new NotImplementedException();
        }
    }
}
