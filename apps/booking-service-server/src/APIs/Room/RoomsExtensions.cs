using BookingService.APIs.Dtos;
using BookingService.Infrastructure.Models;

namespace BookingService.APIs.Extensions;

public static class RoomsExtensions
{
    public static Room ToDto(this RoomDbModel model)
    {
        return new Room
        {
            Bookings = model.Bookings?.Select(x => x.Id).ToList(),
            Capacity = model.Capacity,
            CreatedAt = model.CreatedAt,
            Features = model.Features,
            Id = model.Id,
            RoomNumber = model.RoomNumber,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RoomDbModel ToModel(this RoomUpdateInput updateDto, RoomWhereUniqueInput uniqueId)
    {
        var room = new RoomDbModel
        {
            Id = uniqueId.Id,
            Capacity = updateDto.Capacity,
            Features = updateDto.Features,
            RoomNumber = updateDto.RoomNumber
        };

        if (updateDto.CreatedAt != null)
        {
            room.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            room.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return room;
    }
}
