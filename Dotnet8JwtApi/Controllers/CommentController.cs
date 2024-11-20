using Dotnet8JwtApi.Dtos.Comment;
using Dotnet8JwtApi.Interfaces;
using Dotnet8JwtApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8JwtApi.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments = await commentRepository.GetAllAsync();
        var result = comments.Select(comment => comment.ToCommentDto());

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetComment(int id)
    {
        var comment = await commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto createCommentRequestDto)
    {
        if (!await stockRepository.ExistsByIdAsync(stockId))
        {
            return BadRequest("Stock Not Found");
        }
        
        var comment = createCommentRequestDto.ToComment();
        comment.StockId = stockId;
        await commentRepository.CreateAsync(comment);
        
        return CreatedAtAction(nameof(GetComment), new {id = comment.Id}, comment.ToCommentDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
    {
        var comment = await commentRepository.UpdateAsync(id, updateCommentRequestDto);
        if (comment == null)
        {
            return NotFound("Comment Not Found");
        }
        
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var comment = await commentRepository.DeleteAsync(id);
        if (comment == null)
        {
            return NotFound("Comment Not Found");
        }
        
        return NoContent();
    }
}