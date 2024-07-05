using ModelLayer.DTO.Request.Order;
using ModelLayer.DTO.Response.Order;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface IOrderRepository
    {
        Task<OrderRespone> CreateOrder(CreateOrder request);
        Task<List<Order>> GetAllOrderAsync();
        Task<OrderRespone> GetOrderById (int id);
        Task AddOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<OrderRespone> UpdateOrder(UpdateOrder request);
        Task<Order> GetOrderByAccountId(int accountId);
    }
}
