using Dotnet8JwtApi.Dtos.Comment;
using Dotnet8JwtApi.Models;

namespace Dotnet8JwtApi.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId,
        };
    }

    public static Comment ToComment(this CreateCommentDto createCommentDto)
    {
        return new Comment
        {
            Title = createCommentDto.Title,
            Content = createCommentDto.Content
        };
    }
}