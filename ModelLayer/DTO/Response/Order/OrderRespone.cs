using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Response.Order
{
    public class OrderRespone
    {
        public int OrderId { get; set; }
        public int? AccountId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateBuy { get; set; }
    }
}
