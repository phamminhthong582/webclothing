﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Response.StockDetail
{
	public class StockDetailResponseDTO
	{
		public int StockDetailId { get; set; }
		public int? StockId { get; set; }
		public int? ProductId { get; set; }
		public string? ProductName { get; set; }
		public int? Quantity { get; set; }
		public double? Price { get; set; }
		public double? Total { get; set; }
	}
}
