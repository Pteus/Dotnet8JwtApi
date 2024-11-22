using Dotnet8JwtApi.Dtos.Account;
using Dotnet8JwtApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Dotnet8JwtApi.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(UserManager<AppUser> userManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };

            var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                return roleResult.Succeeded ? Ok("User created successfully") : StatusCode(500, roleResult.Errors);
            }

            return StatusCode(500, createdUser.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}