namespace Dotnet8JwtApi.Helpers;

public class QueryParamsObject
{
    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
}