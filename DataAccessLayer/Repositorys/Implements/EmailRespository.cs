using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Castle.Core.Resource;
using ModelLayer.DTO.Response.Order;

namespace DataAccessLayer.Repositorys.Implements
{
    public class EmailRespository : IEmailRespository
    {
        private readonly WebCoustemClothingContext _context;
        private readonly IConfiguration _configuration;

        public EmailRespository(WebCoustemClothingContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(_configuration["EmailSettings:SmtpUsername"], _configuration["EmailSettings:SmtpPassword"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:FromEmail"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendOrderConfirmationEmailAsync(int orderId, string toEmail)
        {
            var order = await _context.Orders
                .Include(o => o.Account)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            var subject = $"Order Confirmation - #{order.OrderId}";
            var message = new StringBuilder();
            message.AppendLine($"<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Dear {order.Account?.UserName ?? "Valued Customer"},</p>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Thank you for your order!</p>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'><strong>Order Details:</strong></p>");
            message.AppendLine("<table style='border-collapse: collapse; width: 100%; font-family: Arial, sans-serif;'>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <th style='border: 1px solid #ddd; padding: 12px; background-color: #f4f4f4; text-align: left;'>Item</th>");
            message.AppendLine("    <th style='border: 1px solid #ddd; padding: 12px; background-color: #f4f4f4; text-align: left;'>Details</th>");
            message.AppendLine("  </tr>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <td style='border: 1px solid #ddd; padding: 12px;'><strong>Order ID:</strong></td>");
            message.AppendLine($"    <td style='border: 1px solid #ddd; padding: 12px;'>{order.OrderId}</td>");
            message.AppendLine("  </tr>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <td style='border: 1px solid #ddd; padding: 12px;'><strong>Address:</strong></td>");
            message.AppendLine($"    <td style='border: 1px solid #ddd; padding: 12px;'>{order.Address ?? "N/A"}</td>");
            message.AppendLine("  </tr>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <td style='border: 1px solid #ddd; padding: 12px;'><strong>Phone Number:</strong></td>");
            message.AppendLine($"    <td style='border: 1px solid #ddd; padding: 12px;'>{order.PhoneNumber ?? "N/A"}</td>");
            message.AppendLine("  </tr>");
            message.AppendLine("</table>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Thank you for shopping with us!</p>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Best regards,<br>Your Company Name</p>");

            await SendEmailAsync(toEmail, subject, message.ToString());
        }

        public async Task SendOrderUpdateEmailAsync(int orderId, OrderRespone orderResponse)
        {
            // Tìm kiếm đơn hàng dựa trên orderId
            var order = await _context.Orders
                .Include(o => o.Account)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            // Lấy địa chỉ email của tài khoản từ đối tượng order
            var toEmail = order.Account?.Email;
            if (string.IsNullOrEmpty(toEmail))
            {
                throw new Exception("Account email not found.");
            }

            var subject = $"Order Update - #{order.OrderId}";
            var message = new StringBuilder();
            message.AppendLine($"<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Dear {order.Account?.UserName ?? "Valued Customer"},</p>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Your order has been updated.</p>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'><strong>Updated Order Details:</strong></p>");
            message.AppendLine("<table style='border-collapse: collapse; width: 100%; font-family: Arial, sans-serif;'>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <th style='border: 1px solid #ddd; padding: 12px; background-color: #f4f4f4; text-align: left;'>Item</th>");
            message.AppendLine("    <th style='border: 1px solid #ddd; padding: 12px; background-color: #f4f4f4; text-align: left;'>Details</th>");
            message.AppendLine("  </tr>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <td style='border: 1px solid #ddd; padding: 12px;'><strong>Order ID:</strong></td>");
            message.AppendLine($"    <td style='border: 1px solid #ddd; padding: 12px;'>{orderResponse.OrderId}</td>");
            message.AppendLine("  </tr>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <td style='border: 1px solid #ddd; padding: 12px;'><strong>Address:</strong></td>");
            message.AppendLine($"    <td style='border: 1px solid #ddd; padding: 12px;'>{orderResponse.Address ?? "N/A"}</td>");
            message.AppendLine("  </tr>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <td style='border: 1px solid #ddd; padding: 12px;'><strong>Phone Number:</strong></td>");
            message.AppendLine($"    <td style='border: 1px solid #ddd; padding: 12px;'>{orderResponse.PhoneNumber ?? "N/A"}</td>");
            message.AppendLine("  </tr>");
            message.AppendLine("  <tr>");
            message.AppendLine("    <td style='border: 1px solid #ddd; padding: 12px;'><strong>Date of Purchase:</strong></td>");
            message.AppendLine($"    <td style='border: 1px solid #ddd; padding: 12px;'>{orderResponse.DateBuy?.ToString("d MMM yyyy") ?? "N/A"}</td>");
            message.AppendLine("  </tr>");
            message.AppendLine("</table>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Thank you for staying with us!</p>");
            message.AppendLine("<p style='font-family: Arial, sans-serif; font-size: 16px; color: #333;'>Best regards,<br>Your Company Name</p>");

            await SendEmailAsync(toEmail, subject, message.ToString());
        }


    }

}
