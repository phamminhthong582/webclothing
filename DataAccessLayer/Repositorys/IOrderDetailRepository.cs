using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetAllOrderDetailAsync();
        Task<OrderDetail> GetOrderDetailByIdAsync(int id);
        Task AddOrderDetailAsync(OrderDetail orderDetail);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task DeleteOrderDetailAsync(int id);
        Task<List<OrderDetail>> GetOrderDetailByAccountId(int accountId);
        Task<List<OrderDetail>> GetProductByOrderId(int orderid);
    }
}
