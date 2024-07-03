using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request.Supplier
{
    public class UpdateSupplierRequest
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Bank { get; set; }
        public string? BankNumber { get; set; }
        public DateTime? CreateDay { get; set; }
        public bool? Active { get; set; }
    }
}
