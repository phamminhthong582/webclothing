using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Response.Category
{
    public class CategoryRespone
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? CreateDay { get; set; }
    }
}
