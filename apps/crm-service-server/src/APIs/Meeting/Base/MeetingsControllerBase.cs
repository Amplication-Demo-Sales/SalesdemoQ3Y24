using CrmService.APIs;
using CrmService.APIs.Common;
using CrmService.APIs.Dtos;
using CrmService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MeetingsControllerBase : ControllerBase
{
    protected readonly IMeetingsService _service;

    public MeetingsControllerBase(IMeetingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Meeting
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Meeting>> CreateMeeting(MeetingCreateInput input)
    {
        var meeting = await _service.CreateMeeting(input);

        return CreatedAtAction(nameof(Meeting), new { id = meeting.Id }, meeting);
    }

    /// <summary>
    /// Delete one Meeting
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteMeeting([FromRoute()] MeetingWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteMeeting(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Meetings
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Meeting>>> Meetings(
        [FromQuery()] MeetingFindManyArgs filter
    )
    {
        return Ok(await _service.Meetings(filter));
    }

    /// <summary>
    /// Meta data about Meeting records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> MeetingsMeta(
        [FromQuery()] MeetingFindManyArgs filter
    )
    {
        return Ok(await _service.MeetingsMeta(filter));
    }

    /// <summary>
    /// Get one Meeting
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Meeting>> Meeting([FromRoute()] MeetingWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Meeting(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Meeting
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateMeeting(
        [FromRoute()] MeetingWhereUniqueInput uniqueId,
        [FromQuery()] MeetingUpdateInput meetingUpdateDto
    )
    {
        try
        {
            await _service.UpdateMeeting(uniqueId, meetingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Meeting
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] MeetingWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Get a Lead record for Meeting
    /// </summary>
    [HttpGet("{Id}/lead")]
    public async Task<ActionResult<List<Lead>>> GetLead(
        [FromRoute()] MeetingWhereUniqueInput uniqueId
    )
    {
        var lead = await _service.GetLead(uniqueId);
        return Ok(lead);
    }
}
