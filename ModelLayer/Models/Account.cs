using System;
using System.Collections.Generic;

namespace ModelLayer.Models
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
            Stocks = new HashSet<Stock>();
        }

        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? CreateDay { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        //public object Status { get; set; }
    }
}
