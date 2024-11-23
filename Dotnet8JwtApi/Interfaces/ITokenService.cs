using Dotnet8JwtApi.Models;

namespace Dotnet8JwtApi.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}