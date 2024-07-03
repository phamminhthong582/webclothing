using ModelLayer.Enum;
using System;
using System.Collections.Generic;

namespace ModelLayer.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            StockDetails = new HashSet<StockDetail>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Image { get; set; }
        public string? Desciption { get; set; }
        public  string? Size{ get; set; }
        public string? Color { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreateDay { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<StockDetail> StockDetails { get; set; }
    }
}
