using BookingService.APIs;
using BookingService.Infrastructure;
using BookingService.APIs.Dtos;
using BookingService.Infrastructure.Models;
using BookingService.APIs.Errors;
using BookingService.APIs.Extensions;
using BookingService.APIs.Common;
using Microsoft.EntityFrameworkCore;

namespace BookingService.APIs;

public abstract class EventsServiceBase : IEventsService
{
    protected readonly BookingServiceDbContext _context;
    public EventsServiceBase(BookingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Event
    /// </summary>
    public async Task<Event> CreateEvent(EventCreateInput createDto)
    {
        var event = new EventDbModel
                    {
                CreatedAt = createDto.CreatedAt,
Date = createDto.Date,
EventName = createDto.EventName,
Location = createDto.Location,
Notes = createDto.Notes,
UpdatedAt = createDto.UpdatedAt
};
      
            if (createDto.Id != null){
              event.Id = createDto.Id;
}
            if (createDto.Bookings != null)
              {
                  event.Bookings = await _context
                      .Bookings.Where(booking => createDto.Bookings.Select(t => t.Id).Contains(booking.Id))
                      .ToListAsync();
}

_context.Events.Add(event);
await _context.SaveChangesAsync();

var result = await _context.FindAsync<EventDbModel>(event.Id);
      
              if (result == null)
              {
    throw new NotFoundException();
}
      
              return result.ToDto();
}

/// <summary>
/// Delete one Event
/// </summary>
public async Task DeleteEvent(EventWhereUniqueInput uniqueId)
{
    var event = await _context.Events.FindAsync(uniqueId.Id);
    if (event == null)
        {
        throw new NotFoundException();
    }

    _context.Events.Remove(event);
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find many Events
/// </summary>
public async Task<List<Event>> Events(EventFindManyArgs findManyArgs)
{
    var events = await _context
          .Events
  .Include(x => x.Bookings)
  .ApplyWhere(findManyArgs.Where)
  .ApplySkip(findManyArgs.Skip)
  .ApplyTake(findManyArgs.Take)
  .ApplyOrderBy(findManyArgs.SortBy)
  .ToListAsync();
    return events.ConvertAll(event => event.ToDto());
}

/// <summary>
/// Meta data about Event records
/// </summary>
public async Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs)
{

    var count = await _context
.Events
.ApplyWhere(findManyArgs.Where)
.CountAsync();

    return new MetadataDto { Count = count };
}

/// <summary>
/// Get one Event
/// </summary>
public async Task<Event> Event(EventWhereUniqueInput uniqueId)
{
    var events = await this.Events(
              new EventFindManyArgs { Where = new EventWhereInput { Id = uniqueId.Id } }
  );
    var event = events.FirstOrDefault();
    if (event == null)
      {
        throw new NotFoundException();
    }

    return event;
}

/// <summary>
/// Update one Event
/// </summary>
public async Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto)
{
    var event = updateDto.ToModel(uniqueId);

    if (updateDto.Bookings != null)
    {
                  event.Bookings = await _context
                      .Bookings.Where(booking => updateDto.Bookings.Select(t => t).Contains(booking.Id))
                      .ToListAsync();
    }

    _context.Entry(event).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Events.Any(e => e.Id == event.Id))
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
/// Connect multiple Bookings records to Event
/// </summary>
public async Task ConnectBookings(EventWhereUniqueInput uniqueId, BookingWhereUniqueInput[] childrenIds)
{
    var parent = await _context
          .Events.Include(x => x.Bookings)
  .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Bookings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();
    if (children.Count == 0)
    {
        throw new NotFoundException();
    }

    var childrenToConnect = children.Except(parent.Bookings);

    foreach (var child in childrenToConnect)
    {
        parent.Bookings.Add(child);
    }

    await _context.SaveChangesAsync();
}

/// <summary>
/// Disconnect multiple Bookings records from Event
/// </summary>
public async Task DisconnectBookings(EventWhereUniqueInput uniqueId, BookingWhereUniqueInput[] childrenIds)
{
    var parent = await _context
                            .Events.Include(x => x.Bookings)
                    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (parent == null)
    {
        throw new NotFoundException();
    }

    var children = await _context
      .Bookings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
      .ToListAsync();

    foreach (var child in children)
    {
        parent.Bookings?.Remove(child);
    }
    await _context.SaveChangesAsync();
}

/// <summary>
/// Find multiple Bookings records for Event
/// </summary>
public async Task<List<Booking>> FindBookings(EventWhereUniqueInput uniqueId, BookingFindManyArgs eventFindManyArgs)
{
    var bookings = await _context
          .Bookings
  .Where(m => m.EventId == uniqueId.Id)
  .ApplyWhere(eventFindManyArgs.Where)
  .ApplySkip(eventFindManyArgs.Skip)
  .ApplyTake(eventFindManyArgs.Take)
  .ApplyOrderBy(eventFindManyArgs.SortBy)
  .ToListAsync();

    return bookings.Select(x => x.ToDto()).ToList();
}

/// <summary>
/// Update multiple Bookings records for Event
/// </summary>
public async Task UpdateBookings(EventWhereUniqueInput uniqueId, BookingWhereUniqueInput[] childrenIds)
{
    var event = await _context
            .Events.Include(t => t.Bookings)
    .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
    if (event == null)
      {
        throw new NotFoundException();
    }

    var children = await _context
      .Bookings.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
      .ToListAsync();

    if (children.Count == 0)
    {
        throw new NotFoundException();
    }
  
      event.Bookings = children;
    await _context.SaveChangesAsync();
}

}
