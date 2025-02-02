using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;    
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var stocks = await _stockRepository.GetAllAsync();
                
            var stockDto = stocks.Select(s => s.ToStockDto());
        
            return Ok(stocks);
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var stock = await _context.Stock.FindAsync(id);

            if (stock == null) { return NotFound(); }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) {
            var stockCreated = await _stockRepository.Create(stockDto);

            return CreatedAtAction(nameof(GetById), new { id = stockCreated.Id }, stockCreated.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateStockDto updateDto, [FromRoute] int id) {
            var stockUpdated = await _stockRepository.Update(id, updateDto);

            if (stockUpdated == null) return NotFound();
            
            return Ok(stockUpdated.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var deletedStock = await _stockRepository.Delete(id); 
            
            if (deletedStock == null) return NotFound();

            return NoContent();
        }
    }
}