using ModelLayer.DTO.Response.StockDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Response.Stock
{
	public class StockResponseDTO
	{
		public int StockId { get; set; }
		public int? SupplierId { get; set; }
		public int? AccountId { get; set; }
		public DateTime? DateAdd { get; set; }
		public string? GhiChu { get; set; }
		public bool? Status { get; set; }
		public DateTime? DatePayment { get; set; }
		public bool? StatusPayment { get; set; }
		public List<StockDetailResponseDTO> StockDetails { get; set; }
	}
}
