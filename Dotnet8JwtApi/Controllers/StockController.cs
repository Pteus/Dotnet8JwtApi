using Dotnet8JwtApi.Data;
using Dotnet8JwtApi.Dtos.Stock;
using Dotnet8JwtApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8JwtApi.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks =  dbContext.Stocks.ToList()
            .Select(s => s.ToStockDto());
        
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = dbContext.Stocks.Find(id);
        if (stock == null)
        {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto createStockRequestDto)
    {
        var stockModel = createStockRequestDto.ToStockFromCreateDto();
        dbContext.Stocks.Add(stockModel);
        dbContext.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stockModel = dbContext.Stocks.Find(id);
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

        dbContext.SaveChanges();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stockModel = dbContext.Stocks.Find(id);
        if (stockModel == null)
        {
            return NotFound();
        }
        
        dbContext.Stocks.Remove(stockModel);
        dbContext.SaveChanges();
        
        return NoContent();
    }
}