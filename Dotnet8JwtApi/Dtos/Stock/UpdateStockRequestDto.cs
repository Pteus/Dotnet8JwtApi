using System.ComponentModel.DataAnnotations;

namespace Dotnet8JwtApi.Dtos.Stock;

public class UpdateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot exceed 10 characters")]
    public string Symbol { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10, ErrorMessage = "Company name cannot exceed 10 characters")]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    [Range(1, 1_000_000_000)]
    public decimal Purchase { get; set; }
    
    [Required]
    [Range(0.001, 100)]
    public decimal LastDividend { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Industry cannot exceed 10 characters")]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1, 5_000_000_000)]
    public long MarketCap { get; set; }
}