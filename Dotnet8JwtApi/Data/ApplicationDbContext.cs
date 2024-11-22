using Dotnet8JwtApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8JwtApi.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var roles = new List<IdentityRole>
        {
           new()
           {
               Name = "Admin",
               NormalizedName = "ADMIN"
           },
           new()
           {
               Name = "User",
               NormalizedName = "USER"
           }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}