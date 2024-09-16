using BookingService.APIs;
using BookingService.APIs.Common;
using BookingService.APIs.Dtos;
using BookingService.APIs.Errors;
using BookingService.APIs.Extensions;
using BookingService.Infrastructure;
using BookingService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.APIs;

public abstract class RoomsServiceBase : IRoomsService
{
    protected readonly BookingServiceDbContext _context;

    public RoomsServiceBase(BookingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Room
    /// </summary>
    public async Task<Room> CreateRoom(RoomCreateInput createDto)
    {
        var room = new RoomDbModel
        {
            Capacity = createDto.Capacity,
            CreatedAt = createDto.CreatedAt,
            Features = createDto.Features,
            RoomNumber = createDto.RoomNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            room.Id = createDto.Id;
        }
        if (createDto.Bookings != null)
        {
            room.Bookings = await _context
                .Bookings.Where(booking =>
                    createDto.Bookings.Select(t => t.Id).Contains(booking.Id)
                )
                .ToListAsync();
        }

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RoomDbModel>(room.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Room
    /// </summary>
    public async Task DeleteRoom(RoomWhereUniqueInput uniqueId)
    {
        var room = await _context.Rooms.FindAsync(uniqueId.Id);
        if (room == null)
        {
            throw new NotFoundException();
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Rooms
    /// </summary>
    public async Task<List<Room>> Rooms(RoomFindManyArgs findManyArgs)
    {
        var rooms = await _context
            .Rooms.Include(x => x.Bookings)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return rooms.ConvertAll(room => room.ToDto());
    }

    /// <summary>
    /// Meta data about Room records
    /// </summary>
    public async Task<MetadataDto> RoomsMeta(RoomFindManyArgs findManyArgs)
    {
        var count = await _context.Rooms.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Room
    /// </summary>
    public async Task<Room> Room(RoomWhereUniqueInput uniqueId)
    {
        var rooms = await this.Rooms(
            new RoomFindManyArgs { Where = new RoomWhereInput { Id = uniqueId.Id } }
        );
        var room = rooms.FirstOrDefault();
        if (room == null)
        {
            throw new NotFoundException();
        }

        return room;
    }

    /// <summary>
    /// Update one Room
    /// </summary>
    public async Task UpdateRoom(RoomWhereUniqueInput uniqueId, RoomUpdateInput updateDto)
    {
        var room = updateDto.ToModel(uniqueId);

        if (updateDto.Bookings != null)
        {
            room.Bookings = await _context
                .Bookings.Where(booking => updateDto.Bookings.Select(t => t).Contains(booking.Id))
                .ToListAsync();
        }

        _context.Entry(room).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Rooms.Any(e => e.Id == room.Id))
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
    /// Connect multiple Bookings records to Room
    /// </summary>
    public async Task ConnectBookings(
        RoomWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rooms.Include(x => x.Bookings)
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
    /// Disconnect multiple Bookings records from Room
    /// </summary>
    public async Task DisconnectBookings(
        RoomWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Rooms.Include(x => x.Bookings)
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
    /// Find multiple Bookings records for Room
    /// </summary>
    public async Task<List<Booking>> FindBookings(
        RoomWhereUniqueInput uniqueId,
        BookingFindManyArgs roomFindManyArgs
    )
    {
        var bookings = await _context
            .Bookings.Where(m => m.RoomId == uniqueId.Id)
            .ApplyWhere(roomFindManyArgs.Where)
            .ApplySkip(roomFindManyArgs.Skip)
            .ApplyTake(roomFindManyArgs.Take)
            .ApplyOrderBy(roomFindManyArgs.SortBy)
            .ToListAsync();

        return bookings.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Bookings records for Room
    /// </summary>
    public async Task UpdateBookings(
        RoomWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var room = await _context
            .Rooms.Include(t => t.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (room == null)
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

        room.Bookings = children;
        await _context.SaveChangesAsync();
    }
}
