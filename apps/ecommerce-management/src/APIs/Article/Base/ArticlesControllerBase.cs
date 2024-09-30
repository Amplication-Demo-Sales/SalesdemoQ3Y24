using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ArticlesControllerBase : ControllerBase
{
    protected readonly IArticlesService _service;

    public ArticlesControllerBase(IArticlesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Article
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Article>> CreateArticle(ArticleCreateInput input)
    {
        var article = await _service.CreateArticle(input);

        return CreatedAtAction(nameof(Article), new { id = article.Id }, article);
    }

    /// <summary>
    /// Delete one Article
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteArticle([FromRoute()] ArticleWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteArticle(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Articles
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Article>>> Articles(
        [FromQuery()] ArticleFindManyArgs filter
    )
    {
        return Ok(await _service.Articles(filter));
    }

    /// <summary>
    /// Meta data about Article records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ArticlesMeta(
        [FromQuery()] ArticleFindManyArgs filter
    )
    {
        return Ok(await _service.ArticlesMeta(filter));
    }

    /// <summary>
    /// Get one Article
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Article>> Article([FromRoute()] ArticleWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Article(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Article
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateArticle(
        [FromRoute()] ArticleWhereUniqueInput uniqueId,
        [FromQuery()] ArticleUpdateInput articleUpdateDto
    )
    {
        try
        {
            await _service.UpdateArticle(uniqueId, articleUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Comments records to Article
    /// </summary>
    [HttpPost("{Id}/comments")]
    public async Task<ActionResult> ConnectComments(
        [FromRoute()] ArticleWhereUniqueInput uniqueId,
        [FromQuery()] CommentWhereUniqueInput[] commentsId
    )
    {
        try
        {
            await _service.ConnectComments(uniqueId, commentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Comments records from Article
    /// </summary>
    [HttpDelete("{Id}/comments")]
    public async Task<ActionResult> DisconnectComments(
        [FromRoute()] ArticleWhereUniqueInput uniqueId,
        [FromBody()] CommentWhereUniqueInput[] commentsId
    )
    {
        try
        {
            await _service.DisconnectComments(uniqueId, commentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Comments records for Article
    /// </summary>
    [HttpGet("{Id}/comments")]
    public async Task<ActionResult<List<Comment>>> FindComments(
        [FromRoute()] ArticleWhereUniqueInput uniqueId,
        [FromQuery()] CommentFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindComments(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Comments records for Article
    /// </summary>
    [HttpPatch("{Id}/comments")]
    public async Task<ActionResult> UpdateComments(
        [FromRoute()] ArticleWhereUniqueInput uniqueId,
        [FromBody()] CommentWhereUniqueInput[] commentsId
    )
    {
        try
        {
            await _service.UpdateComments(uniqueId, commentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a User record for Article
    /// </summary>
    [HttpGet("{Id}/user")]
    public async Task<ActionResult<List<User>>> GetUser(
        [FromRoute()] ArticleWhereUniqueInput uniqueId
    )
    {
        var user = await _service.GetUser(uniqueId);
        return Ok(user);
    }
}
