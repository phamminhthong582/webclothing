using ModelLayer.DTO.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface IEmailRespository
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

        Task SendOrderConfirmationEmailAsync(int orderId, string toEmail);

        Task SendOrderUpdateEmailAsync(int orderId, OrderRespone orderResponse);



    }
}
