using BookingService.APIs.Dtos;
using BookingService.APIs.Common;

namespace BookingService.APIs;

public interface IEventsService
{
    /// <summary>
    /// Create one Event
    /// </summary>
    public Task<Event> CreateEvent(EventCreateInput event);
    /// <summary>
    /// Delete one Event
    /// </summary>
    public Task DeleteEvent(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Find many Events
    /// </summary>
    public Task<List<Event>> Events(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Meta data about Event records
    /// </summary>
    public Task<MetadataDto> EventsMeta(EventFindManyArgs findManyArgs);
    /// <summary>
    /// Get one Event
    /// </summary>
    public Task<Event> Event(EventWhereUniqueInput uniqueId);
    /// <summary>
    /// Update one Event
    /// </summary>
    public Task UpdateEvent(EventWhereUniqueInput uniqueId, EventUpdateInput updateDto);
    /// <summary>
    /// Connect multiple Bookings records to Event
    /// </summary>
    public Task ConnectBookings(EventWhereUniqueInput uniqueId, BookingWhereUniqueInput[] bookingsId);
    /// <summary>
    /// Disconnect multiple Bookings records from Event
    /// </summary>
    public Task DisconnectBookings(EventWhereUniqueInput uniqueId, BookingWhereUniqueInput[] bookingsId);
    /// <summary>
    /// Find multiple Bookings records for Event
    /// </summary>
    public Task<List<Booking>> FindBookings(EventWhereUniqueInput uniqueId, BookingFindManyArgs BookingFindManyArgs);
    /// <summary>
    /// Update multiple Bookings records for Event
    /// </summary>
    public Task UpdateBookings(EventWhereUniqueInput uniqueId, BookingWhereUniqueInput[] bookingsId);
}
