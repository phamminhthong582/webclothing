using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly WebCoustemClothingContext _context;
        public OrderDetailRepository()
        {
            _context = new WebCoustemClothingContext();
        }
        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            _context.OrderDetails.Remove(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>> GetAllOrderDetailAsync()
        {
            return await _context.OrderDetails.Include(c => c.Product).Include(c => c.Order).ThenInclude(c => c.Account).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetailByAccountId(int accountId)
        {
            var orderdetail = await _context.OrderDetails.Include(c => c.Order).ThenInclude(a => a.Account).Include(c => c.Product).Where(c => c.Product.CategoryId == accountId).ToListAsync();
            return orderdetail;
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task<List<OrderDetail>> GetProductByOrderId(int orderid)
        {
            var orderDetails = await _context.OrderDetails
           .Where(od => od.OrderId == orderid)
           .ToListAsync();

            var productid = orderDetails.Select(od =>  od.ProductId).ToList();
            return orderDetails;          
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.Entry(orderDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
