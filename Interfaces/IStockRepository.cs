using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Model;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetById(int id);
        Task<Stock?> Update(int id, UpdateStockDto stockDto);
        Task<Stock?> Delete(int id);
        Task<Stock> Create(CreateStockRequestDto stock);
        Task<bool> StockExists(int id);
    }
}