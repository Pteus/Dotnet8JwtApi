using Dotnet8JwtApi.Data;
using Dotnet8JwtApi.Dtos.Stock;
using Dotnet8JwtApi.Interfaces;
using Dotnet8JwtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8JwtApi.Repository;

public class StockRepository(ApplicationDbContext dbContext) : IStockRepository
{
    public async Task<List<Stock>> GetAllAsync()
    {
        return await dbContext.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await dbContext.Stocks.FindAsync(id);
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
}