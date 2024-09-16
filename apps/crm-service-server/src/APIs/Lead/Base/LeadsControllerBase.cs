using CrmService.APIs;
using CrmService.APIs.Common;
using CrmService.APIs.Dtos;
using CrmService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class LeadsControllerBase : ControllerBase
{
    protected readonly ILeadsService _service;

    public LeadsControllerBase(ILeadsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Lead
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Lead>> CreateLead(LeadCreateInput input)
    {
        var lead = await _service.CreateLead(input);

        return CreatedAtAction(nameof(Lead), new { id = lead.Id }, lead);
    }

    /// <summary>
    /// Delete one Lead
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteLead([FromRoute()] LeadWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteLead(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Leads
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Lead>>> Leads([FromQuery()] LeadFindManyArgs filter)
    {
        return Ok(await _service.Leads(filter));
    }

    /// <summary>
    /// Meta data about Lead records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> LeadsMeta([FromQuery()] LeadFindManyArgs filter)
    {
        return Ok(await _service.LeadsMeta(filter));
    }

    /// <summary>
    /// Get one Lead
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Lead>> Lead([FromRoute()] LeadWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Lead(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Lead
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateLead(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromQuery()] LeadUpdateInput leadUpdateDto
    )
    {
        try
        {
            await _service.UpdateLead(uniqueId, leadUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Lead
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] LeadWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Connect multiple Meetings records to Lead
    /// </summary>
    [HttpPost("{Id}/meetings")]
    public async Task<ActionResult> ConnectMeetings(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromQuery()] MeetingWhereUniqueInput[] meetingsId
    )
    {
        try
        {
            await _service.ConnectMeetings(uniqueId, meetingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Meetings records from Lead
    /// </summary>
    [HttpDelete("{Id}/meetings")]
    public async Task<ActionResult> DisconnectMeetings(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromBody()] MeetingWhereUniqueInput[] meetingsId
    )
    {
        try
        {
            await _service.DisconnectMeetings(uniqueId, meetingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Meetings records for Lead
    /// </summary>
    [HttpGet("{Id}/meetings")]
    public async Task<ActionResult<List<Meeting>>> FindMeetings(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromQuery()] MeetingFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindMeetings(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Meetings records for Lead
    /// </summary>
    [HttpPatch("{Id}/meetings")]
    public async Task<ActionResult> UpdateMeetings(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromBody()] MeetingWhereUniqueInput[] meetingsId
    )
    {
        try
        {
            await _service.UpdateMeetings(uniqueId, meetingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
