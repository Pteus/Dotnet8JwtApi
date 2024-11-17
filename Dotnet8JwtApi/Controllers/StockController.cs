using Dotnet8JwtApi.Data;
using Dotnet8JwtApi.Dtos.Stock;
using Dotnet8JwtApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8JwtApi.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await dbContext.Stocks.ToListAsync();
        var result = stocks.Select(s => s.ToStockDto());
        
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await dbContext.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockRequestDto)
    {
        var stockModel = createStockRequestDto.ToStockFromCreateDto();
        await dbContext.Stocks.AddAsync(stockModel);
        await dbContext.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stockModel = await dbContext.Stocks.FindAsync(id);
        if (stockModel == null)
        {
            return NotFound();
        }
        
        stockModel.Symbol = updateStockRequestDto.Symbol;
        stockModel.CompanyName = updateStockRequestDto.CompanyName;
        stockModel.Purchase = updateStockRequestDto.Purchase;
        stockModel.LastDividend = updateStockRequestDto.LastDividend;
        stockModel.Industry = updateStockRequestDto.Industry;
        stockModel.MarketCap = updateStockRequestDto.MarketCap;

        await dbContext.SaveChangesAsync();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await dbContext.Stocks.FindAsync(id);
        if (stockModel == null)
        {
            return NotFound();
        }
        
        dbContext.Stocks.Remove(stockModel);
        await dbContext.SaveChangesAsync();
        
        return NoContent();
    }
}