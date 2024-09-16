using Microsoft.AspNetCore.Mvc;
using BookingService.APIs;
using BookingService.APIs.Dtos;
using BookingService.APIs.Errors;
using BookingService.APIs.Common;

namespace BookingService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BookingsControllerBase : ControllerBase
{
    protected readonly IBookingsService _service;
    public BookingsControllerBase(IBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Booking
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Booking>> CreateBooking(BookingCreateInput input)
    {
        var booking = await _service.CreateBooking(input);

        return CreatedAtAction(nameof(Booking), new { id = booking.Id }, booking);
    }

    /// <summary>
    /// Delete one Booking
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteBooking([FromRoute()]
    BookingWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Bookings
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Booking>>> Bookings([FromQuery()]
    BookingFindManyArgs filter)
    {
        return Ok(await _service.Bookings(filter));
    }

    /// <summary>
    /// Meta data about Booking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BookingsMeta([FromQuery()]
    BookingFindManyArgs filter)
    {
        return Ok(await _service.BookingsMeta(filter));
    }

    /// <summary>
    /// Get one Booking
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Booking>> Booking([FromRoute()]
    BookingWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Booking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Booking
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateBooking([FromRoute()]
    BookingWhereUniqueInput uniqueId, [FromQuery()]
    BookingUpdateInput bookingUpdateDto)
    {
        try
        {
            await _service.UpdateBooking(uniqueId, bookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Booking
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer([FromRoute()]
    BookingWhereUniqueInput uniqueId)
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Get a Event record for Booking
    /// </summary>
    [HttpGet("{Id}/event")]
    public async Task<ActionResult<List<Event>>> GetEvent([FromRoute()]
    BookingWhereUniqueInput uniqueId)
    {
        var event = await _service.GetEvent(uniqueId);
            return Ok(event); }

    /// <summary>
    /// Connect multiple Payments records to Booking
    /// </summary>
    [HttpPost("{Id}/payments")]
    public async Task<ActionResult> ConnectPayments([FromRoute()]
    BookingWhereUniqueInput uniqueId, [FromQuery()]
    PaymentWhereUniqueInput[] paymentsId)
    {
        try
        {
            await _service.ConnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Booking
    /// </summary>
    [HttpDelete("{Id}/payments")]
    public async Task<ActionResult> DisconnectPayments([FromRoute()]
    BookingWhereUniqueInput uniqueId, [FromBody()]
    PaymentWhereUniqueInput[] paymentsId)
    {
        try
        {
            await _service.DisconnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Payments records for Booking
    /// </summary>
    [HttpGet("{Id}/payments")]
    public async Task<ActionResult<List<Payment>>> FindPayments([FromRoute()]
    BookingWhereUniqueInput uniqueId, [FromQuery()]
    PaymentFindManyArgs filter)
    {
        try
        {
            return Ok(await _service.FindPayments(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Payments records for Booking
    /// </summary>
    [HttpPatch("{Id}/payments")]
    public async Task<ActionResult> UpdatePayments([FromRoute()]
    BookingWhereUniqueInput uniqueId, [FromBody()]
    PaymentWhereUniqueInput[] paymentsId)
    {
        try
        {
            await _service.UpdatePayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Room record for Booking
    /// </summary>
    [HttpGet("{Id}/room")]
    public async Task<ActionResult<List<Room>>> GetRoom([FromRoute()]
    BookingWhereUniqueInput uniqueId)
    {
        var room = await _service.GetRoom(uniqueId);
        return Ok(room);
    }

}
