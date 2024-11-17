using Dotnet8JwtApi.Data;
using Dotnet8JwtApi.Interfaces;
using Dotnet8JwtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet8JwtApi.Repository;

public class CommentRepository(ApplicationDbContext dbContext) : ICommentRepository
{
    public async Task<List<Comment>> GetAllAsync()
    {
        return await dbContext.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await dbContext.Comments.FindAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await dbContext.Comments.AddAsync(comment);
        await dbContext.SaveChangesAsync();

        return comment;
    }
}