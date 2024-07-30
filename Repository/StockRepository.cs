using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {   
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            var stocks = await _context.Stock.Include(c => c.Comments).ToListAsync();

            return stocks;
        }

        
        public async Task<Stock?> GetById(int id)
        {
           return await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> Delete(int id) {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null) return null;

             _context.Stock.Remove(stock);
             await _context.SaveChangesAsync();

             return stock;
        }

        public async Task<Stock> Create(CreateStockRequestDto stock) {
            var stockFormated = stock.ToStockFromCreationDto();
            await _context.Stock.AddAsync(stockFormated);
            await _context.SaveChangesAsync();

            return stockFormated;
        }

        public async Task<Stock?> Update(int id, UpdateStockDto stockDto) {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null) return null;

            stock.CompanyName = stockDto.CompanyName; 

            await _context.SaveChangesAsync();
            
            return stock;
        }

        public async Task<bool> StockExists(int id) {
            return await _context.Stock.AnyAsync(s => s.Id == id);
        } 
    }
}