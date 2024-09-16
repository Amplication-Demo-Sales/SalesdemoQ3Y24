using BookingService.APIs;
using BookingService.Infrastructure;
using BookingService.APIs.Dtos;
using BookingService.Infrastructure.Models;
using BookingService.APIs.Errors;
using BookingService.APIs.Extensions;
using BookingService.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace BookingService.APIs;

public abstract class BookingsServiceBase : IBookingsService
{
    protected readonly BookingServiceDbContext _context;
    public BookingsServiceBase(BookingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Booking
    /// </summary>
    public async Task<Booking> CreateBooking(BookingCreateInput createDto)
    {
        var booking = new BookingDbModel
        {
            BookingDate = createDto.BookingDate,
            CreatedAt = createDto.CreatedAt,
            Status = createDto.Status,
            TotalAmount = createDto.TotalAmount,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            booking.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            booking.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Event != null)
        {
            booking.Event = await _context
                .Events.Where(event => createDto.Event.Id == event.Id)
                    .FirstOrDefaultAsync();
            }

if (createDto.Payments != null)
              {
                  booking.Payments = await _context
                      .Payments.Where(payment => createDto.Payments.Select(t => t.Id).Contains(payment.Id))
                      .ToListAsync();
              }

if (createDto.Room != null)
{
    booking.Room = await _context
        .Rooms.Where(room => createDto.Room.Id == room.Id)
        .FirstOrDefaultAsync();
}

_context.Bookings.Add(booking);
await _context.SaveChangesAsync();

var result = await _context.FindAsync<BookingDbModel>(booking.Id);

if (result == null)
{
    throw new NotFoundException();
}

return result.ToDto();}

    /// <summary>
    /// Delete one Booking
    /// </summary>
    public async Task DeleteBooking(BookingWhereUniqueInput uniqueId)
{
    var booking = await _context.Bookings.FindAsync(uniqueId.Id);
    if (booking == null)
    {
        throw new NotFoundException();
    }

    _context.Bookings.Remove(booking);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many Bookings
/// </summary>
public async Task<List<Booking>> Bookings(BookingFindManyArgs findManyArgs)
{
    var bookings = await _context
          .Bookings
  .Include(x => x.Customer).Include(x => x.Room).Include(x => x.Event).Include(x => x.Payments)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return bookings.ConvertAll(booking => booking.ToDto());
}

/// <summary>
/// Meta data about Booking records
/// </summary>
public async Task<MetadataDto> BookingsMeta(BookingFindManyArgs findManyArgs)
{

    var count = await _context
.Bookings
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one Booking
/// </summary>
public async Task<Booking> Booking(BookingWhereUniqueInput uniqueId)
{
    var bookings = await this.Bookings(
              new BookingFindManyArgs { Where = new BookingWhereInput { Id = uniqueId.Id } }
  );
    var booking = bookings.FirstOrDefault();
    if (booking == null)
    {
        throw new NotFoundException();
    }

    return booking;
}

/// <summary>
/// Update one Booking
/// </summary>
public async Task UpdateBooking(BookingWhereUniqueInput uniqueId, BookingUpdateInput updateDto)
{
    var booking = updateDto.ToModel(uniqueId);

    if (updateDto.Customer != null)
    {
        booking.Customer = await _context
            .Customers.Where(customer => updateDto.Customer == customer.Id)
            .FirstOrDefaultAsync();
    }

    if (updateDto.Event != null)
    {
        booking.Event = await _context
            .Events.Where(event => updateDto.Event == event.Id)
            .FirstOrDefaultAsync();
    }

    if (updateDto.Payments != null)
    {
        booking.Payments = await _context
            .Payments.Where(payment => updateDto.Payments.Select(t => t).Contains(payment.Id))
            .ToListAsync();
    }

    if (updateDto.Room != null)
    {
        booking.Room = await _context
            .Rooms.Where(room => updateDto.Room == room.Id)
            .FirstOrDefaultAsync();
    }

    _context.Entry(booking).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Bookings.Any(e => e.Id == booking.Id))
        {
            throw new NotFoundException();
        }
        else
        {
            throw;
        }
    }
}

/// <summary>
/// Get a Customer record for Booking
/// </summary>
public async Task<Customer> GetCustomer(BookingWhereUniqueInput uniqueId)
{
    var booking = await _context
          .Bookings.Where(booking => booking.Id == uniqueId.Id)
  .Include(booking => booking.Customer)
  .FirstOrDefaultAsync();
    if (booking == null)
    {
        throw new NotFoundException();
    }
    return booking.Customer.ToDto();
}

/// <summary>
/// Get a Event record for Booking
/// </summary>
public async Task<Event> GetEvent(BookingWhereUniqueInput uniqueId)
{
    var booking = await _context
          .Bookings.Where(booking => booking.Id == uniqueId.Id)
  .Include(booking => booking.Event)
  .FirstOrDefaultAsync();
    if (booking == null)
    {
        throw new NotFoundException();
    }
    return booking.Event.ToDto();
}

/// <summary>
/// Connect multiple Payments records to Booking
/// </summary>
public async Task ConnectPayments(BookingWhereUniqueInput uniqueId, PaymentWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Bookings.Include(x => x.Payments)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.Payments);

    foreach (var child in childrenToConnect)
    {
        parent.Payments.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple Payments records from Booking
/// </summary>
public async Task DisconnectPayments(BookingWhereUniqueInput uniqueId, PaymentWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Bookings.Include(x => x.Payments)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.Payments?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple Payments records for Booking
/// </summary>
public async Task<List<Payment>> FindPayments(BookingWhereUniqueInput uniqueId, PaymentFindManyArgs bookingFindManyArgs)
{
    var payments = await _context
          .Payments
  .Where(m => m.BookingId == uniqueId.Id)
  .ApplyWhere(bookingFindManyArgs.Where)
  .ApplySkip(bookingFindManyArgs.Skip)
  .ApplyTake(bookingFindManyArgs.Take)
  .ApplyOrderBy(bookingFindManyArgs.SortBy)
  .ToListAsync();

    return payments.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple Payments records for Booking
/// </summary>
public async Task UpdatePayments(BookingWhereUniqueInput uniqueId, PaymentWhereUniqueInput[] childrenIds)
{
    var booking = await _context
            .Bookings.Include(t => t.Payments)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (booking == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Payments.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    booking.Payments = children;
    await _context.SaveChangesAsync();
}

/// <summary>
/// Get a Room record for Booking
/// </summary>
public async Task<Room> GetRoom(BookingWhereUniqueInput uniqueId)
{
    var booking = await _context
          .Bookings.Where(booking => booking.Id == uniqueId.Id)
  .Include(booking => booking.Room)
  .FirstOrDefaultAsync();
    if (booking == null)
    {
        throw new NotFoundException();
    }
    return booking.Room.ToDto();
}

}
