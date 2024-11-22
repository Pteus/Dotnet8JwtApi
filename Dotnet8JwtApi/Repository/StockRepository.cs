using Dotnet8JwtApi.Data;
using Dotnet8JwtApi.Dtos.Stock;
using Dotnet8JwtApi.Helpers;
using Dotnet8JwtApi.Interfaces;
using Dotnet8JwtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8JwtApi.Repository;

public class StockRepository(ApplicationDbContext dbContext) : IStockRepository
{
    public async Task<List<Stock>> GetAllAsync(QueryParamsObject queryParams)
    {
        var stocks = dbContext.Stocks
            .Include(c => c.Comments)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.CompanyName))
        {
            stocks = stocks.Where(stock => stock.CompanyName.Contains(queryParams.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Symbol))
        {
            stocks = stocks.Where(stock => stock.Symbol.Contains(queryParams.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(queryParams.SortBy))
        {
            if (queryParams.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = queryParams.IsDescending ? stocks.OrderByDescending(stock => stock.Symbol) : stocks.OrderBy(stock => stock.Symbol);
            }
        }
        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await dbContext.Stocks
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await dbContext.Stocks.AddAsync(stockModel);
        await dbContext.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto)
    {
        var stock = await dbContext.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (stock == null)
        {
            return null;
        }
        
        stock.Symbol = updateStockRequestDto.Symbol;
        stock.CompanyName = updateStockRequestDto.CompanyName;
        stock.Purchase = updateStockRequestDto.Purchase;
        stock.LastDividend = updateStockRequestDto.LastDividend;
        stock.Industry = updateStockRequestDto.Industry;
        stock.MarketCap = updateStockRequestDto.MarketCap;

        await dbContext.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await dbContext.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
        if (stock == null)
        {
            return null;
        }
        
        dbContext.Stocks.Remove(stock);
        await dbContext.SaveChangesAsync();
        
        return stock;
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await dbContext.Stocks.AnyAsync(stock => stock.Id == id);
    }
}