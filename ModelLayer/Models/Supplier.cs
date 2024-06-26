using System;
using System.Collections.Generic;

namespace ModelLayer.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Stocks = new HashSet<Stock>();
        }

        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Bank { get; set; }
        public string? BankNumber { get; set; }
        public DateTime? CreateDay { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
