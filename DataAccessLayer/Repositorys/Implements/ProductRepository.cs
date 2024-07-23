using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Request.Products;
using ModelLayer.DTO.Response;
using ModelLayer.DTO.Response.Account;
using ModelLayer.DTO.Response.Products;
using ModelLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public async Task<ProductRespone> CreateProduct(CreateProductRequest product)
        {
            var pro = new Product
            {            
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                Color = product.Color,
                Size = product.Size,
                Desciption = product.Desciption,              
            }; 
            pro.CreateDay = DateTime.Now;
            await _context.Products.AddAsync(pro);
            await _context.SaveChangesAsync();
            var respone = new ProductRespone
            {
                ProductId = pro.ProductId,
                ProductName = pro.ProductName,
                Price = pro.Price,
                Quantity = pro.Quantity,
                Image = pro.Image,
                Color = pro.Color,
                Size = pro.Size,
                Desciption = pro.Desciption,
                CreateDay = DateTime.Now,
            };
            return respone;
        }
        public async Task DeleteProductAsync(int id)
        {
            var pro = await _context.Products.FindAsync(id);
            if (pro != null)
            {
                _context.Products.Remove(pro);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductRespone> UpdateProduct(UpdateProductRequest request)
        {
            var pro = await _context.Products.FirstOrDefaultAsync(a => a.ProductId == request.ProductId);
            if (pro != null)
            {
                pro.ProductName = request.ProductName;
                pro.Price = request.Price;
                pro.Quantity = request.Quantity;
                pro.Image = request.Image;
                pro.Color = request.Color;
                pro.Size = request.Size;
            }
            await _context.SaveChangesAsync();
            var respone = new ProductRespone
            {
                ProductId = pro.ProductId,
                ProductName = pro.ProductName,
                Price = pro.Price,
                Quantity = pro.Quantity,
                Image = pro.Image,
                Color = pro.Color,
                Size = pro.Size,
                CategoryId = pro.CategoryId,
                CreateDay = DateTime.Now,
            };
            return respone;
        }
    }
}
