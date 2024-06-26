using System;
using System.Collections.Generic;

namespace ModelLayer.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? CreateDay { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
