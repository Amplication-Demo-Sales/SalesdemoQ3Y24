using Microsoft.AspNetCore.Mvc;
using BookingService.APIs;
using BookingService.APIs.Dtos;
using BookingService.APIs.Errors;
using BookingService.APIs.Common;

namespace BookingService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EventsControllerBase : ControllerBase
{
    protected readonly IEventsService _service;
    public EventsControllerBase(IEventsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Event
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Event>> CreateEvent(EventCreateInput input)
    {
        var event = await _service.CreateEvent(input);
        
    return CreatedAtAction(nameof(Event), new { id = event.Id }, event); }

    /// <summary>
    /// Delete one Event
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEvent([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEvent(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Events
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Event>>> Events([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.Events(filter));
    }

    /// <summary>
    /// Meta data about Event records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EventsMeta([FromQuery()]
    EventFindManyArgs filter)
    {
        return Ok(await _service.EventsMeta(filter));
    }

    /// <summary>
    /// Get one Event
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Event>> Event([FromRoute()]
    EventWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Event(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Event
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEvent([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    EventUpdateInput eventUpdateDto)
    {
        try
        {
            await _service.UpdateEvent(uniqueId, eventUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Bookings records to Event
    /// </summary>
    [HttpPost("{Id}/bookings")]
    public async Task<ActionResult> ConnectBookings([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    BookingWhereUniqueInput[] bookingsId)
    {
        try
        {
            await _service.ConnectBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Bookings records from Event
    /// </summary>
    [HttpDelete("{Id}/bookings")]
    public async Task<ActionResult> DisconnectBookings([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    BookingWhereUniqueInput[] bookingsId)
    {
        try
        {
            await _service.DisconnectBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Bookings records for Event
    /// </summary>
    [HttpGet("{Id}/bookings")]
    public async Task<ActionResult<List<Booking>>> FindBookings([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromQuery()]
    BookingFindManyArgs filter)
    {
        try
        {
            return Ok(await _service.FindBookings(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Bookings records for Event
    /// </summary>
    [HttpPatch("{Id}/bookings")]
    public async Task<ActionResult> UpdateBookings([FromRoute()]
    EventWhereUniqueInput uniqueId, [FromBody()]
    BookingWhereUniqueInput[] bookingsId)
    {
        try
        {
            await _service.UpdateBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

}
