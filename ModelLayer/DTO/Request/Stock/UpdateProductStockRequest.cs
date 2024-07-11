using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request.Stock
{
    public class UpdateProductStockRequest
    {
        public int StockId { get; set; }
        public int? SupplierId { get; set; }
        public int? AccountId { get; set; }

        public string? GhiChu { get; set; }
        public bool? Status { get; set; }
        public bool? StatusPayment { get; set; }
    }
}
