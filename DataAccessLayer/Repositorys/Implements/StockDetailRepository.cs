using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Request.Stock;
using ModelLayer.DTO.Request.StockDetails;
using ModelLayer.DTO.Response.Stock;
using ModelLayer.DTO.Response.StockDetails;
using ModelLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class StockDetailRepository : IStockDetailRepository
    {
        private readonly WebCoustemClothingContext _context;

        public StockDetailRepository(WebCoustemClothingContext context)
        {
            _context = context;
        }

        public async Task<List<StockDetailResponseDTO>> GetAllStockDetails()
        {
            var stockDetails = await _context.StockDetails.ToListAsync();
            var stockDetailResponses = stockDetails.Select(sd => new StockDetailResponseDTO
            {
                StockDetailId = sd.StockDetailId,
                StockId = sd.StockId,
                ProductId = sd.ProductId,
                ProductName = sd.ProductName,
                Quantity = sd.Quantity,
                Price = sd.Price,
                Total = sd.Total
            }).ToList();

            return stockDetailResponses;
        }

        public async Task<StockDetailResponseDTO> GetStockDetailById(int id)
        {
            var stockDetail = await _context.StockDetails.FindAsync(id);
            if (stockDetail != null)
            {
                return new StockDetailResponseDTO
                {
                    StockDetailId = stockDetail.StockDetailId,
                    StockId = stockDetail.StockId,
                    ProductId = stockDetail.ProductId,
                    ProductName = stockDetail.ProductName,
                    Quantity = stockDetail.Quantity,
                    Price = stockDetail.Price,
                    Total = stockDetail.Total
                };
            }
            return null;
        }

        public async Task<StockDetailResponseDTO> CreateStockDetail(CreateStockDetailRequest request)
        {
            var stockDetail = new StockDetail
            {
                StockId = request.StockId,
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                Price = request.Price,
                Total = request.Price * request.Quantity
        };

            await _context.StockDetails.AddAsync(stockDetail);
            await _context.SaveChangesAsync();

            return new StockDetailResponseDTO
            {
                StockDetailId = stockDetail.StockDetailId,
                StockId = stockDetail.StockId,
                ProductId = stockDetail.ProductId,
                ProductName = stockDetail.ProductName,
                Quantity = stockDetail.Quantity,
                Price = stockDetail.Price,
                Total = stockDetail.Total
            };
        }

        public async Task<StockDetailResponseDTO> UpdateStockDetail(UpdateStockDetailRequest request)
        {
            var stockDetail = await _context.StockDetails.FindAsync(request.StockDetailId);
            if (stockDetail != null)
            {
                stockDetail.StockId = request.StockId;
                stockDetail.ProductId = request.ProductId;
                stockDetail.ProductName = request.ProductName;
                stockDetail.Quantity = request.Quantity;
                stockDetail.Price = request.Price;
                stockDetail.Total = request.Price * request.Quantity;

                await _context.SaveChangesAsync();

                return new StockDetailResponseDTO
                {
                    StockDetailId = stockDetail.StockDetailId,
                    StockId = stockDetail.StockId,
                    ProductId = stockDetail.ProductId,
                    ProductName = stockDetail.ProductName,
                    Quantity = stockDetail.Quantity,
                    Price = stockDetail.Price,
                    Total = stockDetail.Total
                };
            }
            return null;
        }

        public async Task DeleteStockDetailAsync(int id)
        {
            var stockDetail = await _context.StockDetails.FindAsync(id);
            if (stockDetail != null)
            {
                _context.StockDetails.Remove(stockDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
