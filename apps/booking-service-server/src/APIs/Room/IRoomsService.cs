using BookingService.APIs.Common;
using BookingService.APIs.Dtos;

namespace BookingService.APIs;

public interface IRoomsService
{
    /// <summary>
    /// Create one Room
    /// </summary>
    public Task<Room> CreateRoom(RoomCreateInput room);

    /// <summary>
    /// Delete one Room
    /// </summary>
    public Task DeleteRoom(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Room
    /// </summary>
    public Task<Room> Room(RoomWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Room
    /// </summary>
    public Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Bookings records to Room
    /// </summary>
    public Task ConnectBookings(
        RoomWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Disconnect multiple Bookings records from Room
    /// </summary>
    public Task DisconnectBookings(
        RoomWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Find multiple Bookings records for Room
    /// </summary>
    public Task<List<Booking>> FindBookings(
        RoomWhereUniqueInput uniqueId,
        BookingFindManyArgs BookingFindManyArgs
    );

    /// <summary>
    /// Update multiple Bookings records for Room
    /// </summary>
    public Task UpdateBookings(RoomWhereUniqueInput uniqueId, BookingWhereUniqueInput[] bookingsId);
}
