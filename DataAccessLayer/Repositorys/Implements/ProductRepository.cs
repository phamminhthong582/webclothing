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
            var pro = _context.Products.Include(p => p.Category).Select(a => new ProductRespone
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
                CategoryName = a.Category.CategoryName,
            });        
            return pro.ToList();
        }
        public async Task<ProductRespone> GetProductById(int id)
        {
            var pro = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(a => a.ProductId == id);
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
                    CategoryName = pro.Category.CategoryName,
                };
            }
         
            return null;
        }
        public async Task<ProductRespone> CreateProduct(CreateProductRequest product)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == product.CategoryId);
            var pro = new Product
            {            
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                Color = product.Color,
                Size = product.Size,
                Desciption = product.Desciption,   
                CategoryId= category.CategoryId,
            }; 
            pro.CreateDay = DateTime.Now;
            await _context.Products.AddAsync(pro);
            await _context.SaveChangesAsync();
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
                CategoryName = category.CategoryName,
                CreateDay = DateTime.Now,
            };
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
            var pro = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(a => a.ProductId == request.ProductId);
            if (pro != null)
            {
                pro.ProductName = request.ProductName;
                pro.Price = request.Price;
                pro.Quantity = request.Quantity;
                pro.Image = request.Image;
                pro.Desciption = request.Desciption;
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
                Desciption = pro.Desciption,
                Color = pro.Color,
                Size = pro.Size,
                CategoryName = pro.Category.CategoryName,
                CreateDay = DateTime.Now,
            };
            return respone;
        }
        public async Task<List<ProductRespone>> SearchProductsByNameAsync(string productName)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.ProductName.Contains(productName));
            }
            return await query
                .Select(p => new ProductRespone
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Image = p.Image,
                    Color = p.Color,
                    Size = p.Size,
                    Desciption = p.Desciption,
                    CreateDay = p.CreateDay,
                    CategoryName = p.Category.CategoryName,
                })
                .ToListAsync();
        }

        public async Task<List<ProductRespone>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(a => new ProductRespone
                {
                    ProductName = a.ProductName,
                    Price = a.Price,
                    Quantity = a.Quantity,
                    Image = a.Image,
                    Color = a.Color,
                    Size = a.Size,
                    Desciption = a.Desciption,
                    CreateDay = a.CreateDay,
                    CategoryName = a.Category.CategoryName,
                })
                .ToListAsync();
        }

        public async Task<List<ProductRespone>> GetProductsSortedByPriceAsync(bool sort)
        {
            var query = _context.Products.AsQueryable();
            query = sort ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
            return await query
                .Select(a => new ProductRespone
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
                    CategoryName = a.Category.CategoryName,
                })
                .ToListAsync();
        }
        public async Task<List<ProductRespone>> GetPagedProductsAsync(IEnumerable<ProductRespone> products, int page, int productinpage)
        {
            return products
                .Skip((page - 1) * productinpage)
                .Take(productinpage)
                .ToList();
        }
    }
}
