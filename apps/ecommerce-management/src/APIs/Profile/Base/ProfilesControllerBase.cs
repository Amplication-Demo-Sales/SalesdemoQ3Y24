using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProfilesControllerBase : ControllerBase
{
    protected readonly IProfilesService _service;

    public ProfilesControllerBase(IProfilesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Profile
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Profile>> CreateProfile(ProfileCreateInput input)
    {
        var profile = await _service.CreateProfile(input);

        return CreatedAtAction(nameof(Profile), new { id = profile.Id }, profile);
    }

    /// <summary>
    /// Delete one Profile
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteProfile([FromRoute()] ProfileWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProfile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Profiles
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Profile>>> Profiles(
        [FromQuery()] ProfileFindManyArgs filter
    )
    {
        return Ok(await _service.Profiles(filter));
    }

    /// <summary>
    /// Meta data about Profile records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProfilesMeta(
        [FromQuery()] ProfileFindManyArgs filter
    )
    {
        return Ok(await _service.ProfilesMeta(filter));
    }

    /// <summary>
    /// Get one Profile
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Profile>> Profile([FromRoute()] ProfileWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Profile(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Profile
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateProfile(
        [FromRoute()] ProfileWhereUniqueInput uniqueId,
        [FromQuery()] ProfileUpdateInput profileUpdateDto
    )
    {
        try
        {
            await _service.UpdateProfile(uniqueId, profileUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a User record for Profile
    /// </summary>
    [HttpGet("{Id}/user")]
    public async Task<ActionResult<List<User>>> GetUser(
        [FromRoute()] ProfileWhereUniqueInput uniqueId
    )
    {
        var user = await _service.GetUser(uniqueId);
        return Ok(user);
    }
}
