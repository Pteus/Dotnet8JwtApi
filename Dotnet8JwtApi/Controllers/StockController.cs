using Dotnet8JwtApi.Data;
using Dotnet8JwtApi.Dtos.Stock;
using Dotnet8JwtApi.Interfaces;
using Dotnet8JwtApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8JwtApi.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(IStockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await stockRepository.GetAllAsync();
        var result = stocks.Select(s => s.ToStockDto());
        
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await stockRepository.GetByIdAsync(id);
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
        await stockRepository.CreateAsync(stockModel);
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
    {
        var stockModel = await stockRepository.UpdateAsync(id, updateStockRequestDto);
        if (stockModel == null)
        {
            return NotFound();
        }

        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await stockRepository.DeleteAsync(id);
        if (stockModel == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}