using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CommentsControllerBase : ControllerBase
{
    protected readonly ICommentsService _service;

    public CommentsControllerBase(ICommentsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Comment
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Comment>> CreateComment(CommentCreateInput input)
    {
        var comment = await _service.CreateComment(input);

        return CreatedAtAction(nameof(Comment), new { id = comment.Id }, comment);
    }

    /// <summary>
    /// Delete one Comment
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteComment([FromRoute()] CommentWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteComment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Comments
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Comment>>> Comments(
        [FromQuery()] CommentFindManyArgs filter
    )
    {
        return Ok(await _service.Comments(filter));
    }

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CommentsMeta(
        [FromQuery()] CommentFindManyArgs filter
    )
    {
        return Ok(await _service.CommentsMeta(filter));
    }

    /// <summary>
    /// Get one Comment
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Comment>> Comment([FromRoute()] CommentWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Comment(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Comment
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateComment(
        [FromRoute()] CommentWhereUniqueInput uniqueId,
        [FromQuery()] CommentUpdateInput commentUpdateDto
    )
    {
        try
        {
            await _service.UpdateComment(uniqueId, commentUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Article record for Comment
    /// </summary>
    [HttpGet("{Id}/article")]
    public async Task<ActionResult<List<Article>>> GetArticle(
        [FromRoute()] CommentWhereUniqueInput uniqueId
    )
    {
        var article = await _service.GetArticle(uniqueId);
        return Ok(article);
    }

    /// <summary>
    /// Get a User record for Comment
    /// </summary>
    [HttpGet("{Id}/user")]
    public async Task<ActionResult<List<User>>> GetUser(
        [FromRoute()] CommentWhereUniqueInput uniqueId
    )
    {
        var user = await _service.GetUser(uniqueId);
        return Ok(user);
    }
}
