using BookingService.APIs.Dtos;
using BookingService.Infrastructure.Models;

namespace BookingService.APIs.Extensions;

public static class BookingsExtensions
{
    public static Booking ToDto(this BookingDbModel model)
    {
        return new Booking
        {
            BookingDate = model.BookingDate,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Event = model.EventId,
            Id = model.Id,
            Payments = model.Payments?.Select(x => x.Id).ToList(),
            Room = model.RoomId,
            Status = model.Status,
            TotalAmount = model.TotalAmount,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BookingDbModel ToModel(
        this BookingUpdateInput updateDto,
        BookingWhereUniqueInput uniqueId
    )
    {
        var booking = new BookingDbModel
        {
            Id = uniqueId.Id,
            BookingDate = updateDto.BookingDate,
            Status = updateDto.Status,
            TotalAmount = updateDto.TotalAmount
        };

        if (updateDto.CreatedAt != null)
        {
            booking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            booking.CustomerId = updateDto.Customer;
        }
        if (updateDto.Event != null)
        {
            booking.EventId = updateDto.Event;
        }
        if (updateDto.Room != null)
        {
            booking.RoomId = updateDto.Room;
        }
        if (updateDto.UpdatedAt != null)
        {
            booking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return booking;
    }
}
