using Dotnet8JwtApi.Dtos.Stock;
using Dotnet8JwtApi.Models;

namespace Dotnet8JwtApi.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> ExistsByIdAsync(int id);
}