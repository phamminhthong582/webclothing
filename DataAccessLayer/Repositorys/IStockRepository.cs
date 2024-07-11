using ModelLayer.DTO.Request.Products;
using ModelLayer.DTO.Request.Stock;
using ModelLayer.DTO.Response.Products;
using ModelLayer.DTO.Response.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys
{
    public interface IStockRepository
    {
        Task<List<StockResponseDTO>> GetAllStock();
        Task<StockResponseDTO> GetStockById(int id);
        Task<StockResponseDTO> CreateStock(CreateProductStockRequest request);
        Task<StockResponseDTO> UpdateStock(UpdateProductStockRequest request);
        Task DeleteStockAsync(int id);
    }
}
