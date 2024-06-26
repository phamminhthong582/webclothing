using System;
using System.Collections.Generic;

namespace ModelLayer.Models
{
    public partial class StockDetail
    {
        public int StockDetailId { get; set; }
        public int? StockId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Total { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Stock? Stock { get; set; }
    }
}
