using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UsersControllerBase : ControllerBase
{
    protected readonly IUsersService _service;

    public UsersControllerBase(IUsersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<User>> CreateUser(UserCreateInput input)
    {
        var user = await _service.CreateUser(input);

        return CreatedAtAction(nameof(User), new { id = user.Id }, user);
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUser([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteUser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<User>>> Users([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.Users(filter));
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UsersMeta([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.UsersMeta(filter));
    }

    /// <summary>
    /// Get one User
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<User>> User([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.User(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one User
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] UserUpdateInput userUpdateDto
    )
    {
        try
        {
            await _service.UpdateUser(uniqueId, userUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Articles records to User
    /// </summary>
    [HttpPost("{Id}/articles")]
    public async Task<ActionResult> ConnectArticles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ArticleWhereUniqueInput[] articlesId
    )
    {
        try
        {
            await _service.ConnectArticles(uniqueId, articlesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Articles records from User
    /// </summary>
    [HttpDelete("{Id}/articles")]
    public async Task<ActionResult> DisconnectArticles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ArticleWhereUniqueInput[] articlesId
    )
    {
        try
        {
            await _service.DisconnectArticles(uniqueId, articlesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Articles records for User
    /// </summary>
    [HttpGet("{Id}/articles")]
    public async Task<ActionResult<List<Article>>> FindArticles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ArticleFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindArticles(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Articles records for User
    /// </summary>
    [HttpPatch("{Id}/articles")]
    public async Task<ActionResult> UpdateArticles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ArticleWhereUniqueInput[] articlesId
    )
    {
        try
        {
            await _service.UpdateArticles(uniqueId, articlesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Comments records to User
    /// </summary>
    [HttpPost("{Id}/comments")]
    public async Task<ActionResult> ConnectComments(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Comments records from User
    /// </summary>
    [HttpDelete("{Id}/comments")]
    public async Task<ActionResult> DisconnectComments(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Find multiple Comments records for User
    /// </summary>
    [HttpGet("{Id}/comments")]
    public async Task<ActionResult<List<Comment>>> FindComments(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Update multiple Comments records for User
    /// </summary>
    [HttpPatch("{Id}/comments")]
    public async Task<ActionResult> UpdateComments(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Connect multiple Profiles records to User
    /// </summary>
    [HttpPost("{Id}/profiles")]
    public async Task<ActionResult> ConnectProfiles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ProfileWhereUniqueInput[] profilesId
    )
    {
        try
        {
            await _service.ConnectProfiles(uniqueId, profilesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Profiles records from User
    /// </summary>
    [HttpDelete("{Id}/profiles")]
    public async Task<ActionResult> DisconnectProfiles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ProfileWhereUniqueInput[] profilesId
    )
    {
        try
        {
            await _service.DisconnectProfiles(uniqueId, profilesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Profiles records for User
    /// </summary>
    [HttpGet("{Id}/profiles")]
    public async Task<ActionResult<List<Profile>>> FindProfiles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ProfileFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindProfiles(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Profiles records for User
    /// </summary>
    [HttpPatch("{Id}/profiles")]
    public async Task<ActionResult> UpdateProfiles(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ProfileWhereUniqueInput[] profilesId
    )
    {
        try
        {
            await _service.UpdateProfiles(uniqueId, profilesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
