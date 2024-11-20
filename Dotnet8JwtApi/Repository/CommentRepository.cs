using Dotnet8JwtApi.Data;
using Dotnet8JwtApi.Dtos.Comment;
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

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto comment)
    {
        var existingComment = await dbContext.Comments.FindAsync(id);
        if (existingComment == null)
        {
            return null;
        }
        
        existingComment.Content = comment.Content;
        existingComment.Title = comment.Title;
        
        await dbContext.SaveChangesAsync();
        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await dbContext.Comments.FindAsync(id);
        if (comment == null)
        {
            return null;
        }
        
        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync();
        return comment;
    }
}