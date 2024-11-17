using Dotnet8JwtApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8JwtApi.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(ApplicationDbContext dbContext) : ControllerBase
{

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks =  dbContext.Stocks.ToList();
        
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
        
        return Ok(stock);
    }
    
}