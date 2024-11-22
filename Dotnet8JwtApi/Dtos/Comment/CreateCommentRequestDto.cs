using System.ComponentModel.DataAnnotations;

namespace Dotnet8JwtApi.Dtos.Comment;

public class CreateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title min length is 5 characters")]
    [MaxLength(280, ErrorMessage = "Title max length is 280 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5, ErrorMessage = "Content min length is 5 characters")]
    [MaxLength(280, ErrorMessage = "Content max length is 280 characters")]
    public string Content { get; set; } = string.Empty;
}