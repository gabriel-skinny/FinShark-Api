using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Dtos.Stock;
using api.Model;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel) {
            return new StockDto { 
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
                };
        }

        public static Stock ToStockFromCreationDto(this CreateStockRequestDto stockDto) {
            return new Stock {
                 Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }

        public static Stock ToStockFromUpdate(this UpdateStockDto updateDto, int id) {
            return new Stock{
                Id = id,
                Symbol = updateDto.Symbol,
                CompanyName = updateDto.CompanyName,
                Purchase = updateDto.Purchase,
                LastDiv = updateDto.LastDiv,
                Industry = updateDto.Industry,
                MarketCap = updateDto.MarketCap,
            };
        }
    }
}