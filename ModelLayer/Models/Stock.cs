using System;
using System.Collections.Generic;

namespace ModelLayer.Models
{
    public partial class Stock
    {
        public Stock()
        {
            StockDetails = new HashSet<StockDetail>();
        }

        public int StockId { get; set; }
        public int? SupplierId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? DateAdd { get; set; }
        public string? GhiChu { get; set; }
        public bool? Status { get; set; }
        public DateTime? DatePayment { get; set; }
        public bool? StatusPayment { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<StockDetail> StockDetails { get; set; }
    }
}
