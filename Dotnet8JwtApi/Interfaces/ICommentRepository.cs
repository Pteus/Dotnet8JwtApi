using Dotnet8JwtApi.Dtos.Comment;
using Dotnet8JwtApi.Models;

namespace Dotnet8JwtApi.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto comment); 
    Task<Comment?> DeleteAsync(int id);
}