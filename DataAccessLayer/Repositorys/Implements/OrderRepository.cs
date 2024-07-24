using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Request.Order;
using ModelLayer.DTO.Response.Category;
using ModelLayer.DTO.Response.Order;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebCoustemClothingContext _context;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IEmailRespository _emailRespository;

        public OrderRepository(WebCoustemClothingContext context, IOrderDetailRepository orderDetailRepository, IEmailRespository emailRespository)
        {
            _context = context;
            _orderDetailRepository = orderDetailRepository;
            _emailRespository = emailRespository;
        }     

        public async Task<OrderRespone> CreateOrder(CreateOrder request)
        {
            var order = new Order
            {
                AccountId = request.AccountId,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                DateBuy = DateTime.Now,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            var respone = new OrderRespone
            {
                OrderId = order.OrderId,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                DateBuy = DateTime.Now,
                AccountId = order.AccountId,
            };

            var account = await _context.Accounts.FindAsync(respone.AccountId);
            if (account != null)
            {
                await _emailRespository.SendOrderConfirmationEmailAsync(order.OrderId, account.Email);
            }
            return respone;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllOrderAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByAccountId(int accountId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(a => a.AccountId == accountId);
            return order;
        }
        
        public async Task<OrderRespone> GetOrderById(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == id);
            if (order != null)
            {
                return new OrderRespone
                {
                    OrderId = order.OrderId,
                    AccountId = order.AccountId,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    DateBuy = order.DateBuy,
                };
            }
            return null;
        }

        public async Task<OrderRespone> UpdateOrder(UpdateOrder request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(a => a.OrderId == request.OrderId);
            if (order != null)
            {
                order.Address = request.Address;
                order.PhoneNumber = request.PhoneNumber;
                order.DateBuy = request.DateBuy;
            }
            await _context.SaveChangesAsync();
            var respone = new OrderRespone
            {
                OrderId = request.OrderId,
                AccountId = request.AccountId,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                DateBuy = DateTime.Now
            };
            await _emailRespository.SendOrderUpdateEmailAsync(order.OrderId, respone);
            return respone;
        }
    }
}
