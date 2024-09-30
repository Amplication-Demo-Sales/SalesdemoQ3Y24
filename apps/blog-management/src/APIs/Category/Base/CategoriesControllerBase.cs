using BlogManagement.APIs;
using BlogManagement.APIs.Common;
using BlogManagement.APIs.Dtos;
using BlogManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CategoriesControllerBase : ControllerBase
{
    protected readonly ICategoriesService _service;

    public CategoriesControllerBase(ICategoriesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Category
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Category>> CreateCategory(CategoryCreateInput input)
    {
        var category = await _service.CreateCategory(input);

        return CreatedAtAction(nameof(Category), new { id = category.Id }, category);
    }

    /// <summary>
    /// Delete one Category
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCategory([FromRoute()] CategoryWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCategory(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Categories
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Category>>> Categories(
        [FromQuery()] CategoryFindManyArgs filter
    )
    {
        return Ok(await _service.Categories(filter));
    }

    /// <summary>
    /// Meta data about Category records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CategoriesMeta(
        [FromQuery()] CategoryFindManyArgs filter
    )
    {
        return Ok(await _service.CategoriesMeta(filter));
    }

    /// <summary>
    /// Get one Category
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Category>> Category(
        [FromRoute()] CategoryWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Category(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Category
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCategory(
        [FromRoute()] CategoryWhereUniqueInput uniqueId,
        [FromQuery()] CategoryUpdateInput categoryUpdateDto
    )
    {
        try
        {
            await _service.UpdateCategory(uniqueId, categoryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Products records to Category
    /// </summary>
    [HttpPost("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectProducts(
        [FromRoute()] CategoryWhereUniqueInput uniqueId,
        [FromQuery()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.ConnectProducts(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Products records from Category
    /// </summary>
    [HttpDelete("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectProducts(
        [FromRoute()] CategoryWhereUniqueInput uniqueId,
        [FromBody()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.DisconnectProducts(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Products records for Category
    /// </summary>
    [HttpGet("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Product>>> FindProducts(
        [FromRoute()] CategoryWhereUniqueInput uniqueId,
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindProducts(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Products records for Category
    /// </summary>
    [HttpPatch("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateProducts(
        [FromRoute()] CategoryWhereUniqueInput uniqueId,
        [FromBody()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.UpdateProducts(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
