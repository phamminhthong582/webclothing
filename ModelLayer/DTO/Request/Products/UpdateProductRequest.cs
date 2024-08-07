﻿using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request.Products
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Image { get; set; }
        public string? Desciption { get; set; }
        public string? Size {  get; set; } 
        public string? Color { get; set; }            
    }
}
