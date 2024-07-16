using ModelLayer.DTO.Request.StockDetail;
using ModelLayer.DTO.Response.StockDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
	public interface IStockDetailRepository
	{
		Task<List<StockDetailResponseDTO>> GetAllStockDetails();
		Task<StockDetailResponseDTO> GetStockDetailById(int id);
		Task<StockDetailResponseDTO> CreateStockDetail(CreateStockDetailRequest request);
		Task<StockDetailResponseDTO> UpdateStockDetail(UpdateStockDetailRequest request);
		Task DeleteStockDetailAsync(int id);
	}
}
