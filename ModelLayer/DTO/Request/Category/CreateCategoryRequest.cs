using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request.Category
{
    public class CreateCategoryRequest
    {
        public string? CategoryName { get; set; }
        public DateTime? CreateDay { get; set; }
    }
}
