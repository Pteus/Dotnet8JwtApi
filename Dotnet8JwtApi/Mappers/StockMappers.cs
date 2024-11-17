using Dotnet8JwtApi.Dtos.Stock;
using Dotnet8JwtApi.Models;

namespace Dotnet8JwtApi.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Industry = stockModel.Industry,
            Purchase = stockModel.Purchase,
            LastDividend = stockModel.LastDividend,
            MarketCap = stockModel.MarketCap
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockRequestDto)
    {
        return new Stock
        {
            Symbol = stockRequestDto.Symbol,
            CompanyName = stockRequestDto.CompanyName,
            Industry = stockRequestDto.Industry,
            Purchase = stockRequestDto.Purchase,
            LastDividend = stockRequestDto.LastDividend,
            MarketCap = stockRequestDto.MarketCap
        };
    }
}