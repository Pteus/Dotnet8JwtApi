using Dotnet8JwtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8JwtApi.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}