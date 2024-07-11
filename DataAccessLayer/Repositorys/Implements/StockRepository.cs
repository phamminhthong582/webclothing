using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO.Request.Stock;
using ModelLayer.DTO.Response.Stock;
using ModelLayer.DTO.Response.StockDetails;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositorys.Implements
{
    public class StockRepository : IStockRepository
    {
        private readonly WebCoustemClothingContext _context;

        public StockRepository(WebCoustemClothingContext context)
        {
            _context = context;
        }

        public async Task<StockResponseDTO> CreateStock(CreateProductStockRequest request)
        {
            var stock = new Stock
            {
                SupplierId = request.SupplierId,
                AccountId = request.AccountId,
                DateAdd = DateTime.Now,
                GhiChu = request.GhiChu,
                Status = request.Status,
                DatePayment = DateTime.Now,
                StatusPayment = request.StatusPayment
            };

            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();

            var response = new StockResponseDTO
            {
                StockId = stock.StockId,
                SupplierId = stock.SupplierId,
                AccountId = stock.AccountId,
                DateAdd = stock.DateAdd,
                GhiChu = stock.GhiChu,
                Status = stock.Status,
                DatePayment = stock.DatePayment,
                StatusPayment = stock.StatusPayment

            };

            return response;
        }

        public async Task DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<StockResponseDTO>> GetAllStock()
        {
            var stocks = await _context.Stocks
                                           .Include(s => s.StockDetails) // Nạp thông tin StockDetails
                                           .ToListAsync();
            var stockResponses = new List<StockResponseDTO>();

            foreach (var stock in stocks)
            {
                stockResponses.Add(new StockResponseDTO
                {
                    StockId = stock.StockId,
                    SupplierId = stock.SupplierId,
                    AccountId = stock.AccountId,
                    DateAdd = stock.DateAdd,
                    GhiChu = stock.GhiChu,
                    Status = stock.Status,
                    DatePayment = stock.DatePayment,
                    StatusPayment = stock.StatusPayment,
                     StockDetails = stock.StockDetails.Select(detail => new StockDetailResponseDTO
                     {
                         StockDetailId = detail.StockDetailId,
                         StockId = detail.StockId,
                         ProductId = detail.ProductId,
                         ProductName =detail.ProductName,
                         Quantity = detail.Quantity,
                         Price =detail.Price,
                         Total =detail.Total 

                      }).ToList()
                });
            }

            return stockResponses;
        }

        public async Task<StockResponseDTO> GetStockById(int id)
        {
            var stock = await _context.Stocks
                .Include(s => s.StockDetails)
                .FirstOrDefaultAsync(s => s.StockId == id);

            if (stock != null)
            {
                var stockResponse = new StockResponseDTO
                {
                    StockId = stock.StockId,
                    SupplierId = stock.SupplierId,
                    AccountId = stock.AccountId,
                    DateAdd = stock.DateAdd,
                    GhiChu = stock.GhiChu,
                    Status = stock.Status,
                    DatePayment = stock.DatePayment,
                    StatusPayment = stock.StatusPayment,
                    StockDetails = stock.StockDetails.Select(sd => new StockDetailResponseDTO
                    {
                        StockDetailId = sd.StockDetailId,
                        StockId = sd.StockId,
                        ProductId = sd.ProductId,
                        ProductName = sd.ProductName,
                        Quantity = sd.Quantity,
                        Price = sd.Price,
                        Total = sd.Total
                       
                    }).ToList()
                };

                return stockResponse;
            }

            return null;
        }


        public async Task<StockResponseDTO> UpdateStock(UpdateProductStockRequest request)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.StockId == request.StockId);
            if (stock != null)
            {
                stock.SupplierId = request.SupplierId;
                stock.AccountId = request.AccountId;
                stock.DateAdd = DateTime.Now;
                stock.GhiChu = request.GhiChu;
                stock.Status = request.Status;
                stock.DatePayment = DateTime.Now;
                stock.StatusPayment = request.StatusPayment;

                await _context.SaveChangesAsync();

                var response = new StockResponseDTO
                {
                    StockId = stock.StockId,
                    SupplierId = stock.SupplierId,
                    AccountId = stock.AccountId,
                    DateAdd = stock.DateAdd,
                    GhiChu = stock.GhiChu,
                    Status = stock.Status,
                    DatePayment = stock.DatePayment,
                    StatusPayment = stock.StatusPayment
                };
                return response;
            }
            return null;
        }
    }
}
