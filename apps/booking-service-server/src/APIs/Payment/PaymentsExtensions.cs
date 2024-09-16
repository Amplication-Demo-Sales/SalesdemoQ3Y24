using BookingService.APIs.Dtos;
using BookingService.Infrastructure.Models;

namespace BookingService.APIs.Extensions;

public static class PaymentsExtensions
{
    public static Payment ToDto(this PaymentDbModel model)
    {
        return new Payment
        {
            Amount = model.Amount,
            Booking = model.BookingId,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            PaymentDate = model.PaymentDate,
            PaymentMethod = model.PaymentMethod,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PaymentDbModel ToModel(
        this PaymentUpdateInput updateDto,
        PaymentWhereUniqueInput uniqueId
    )
    {
        var payment = new PaymentDbModel
        {
            Id = uniqueId.Id,
            Amount = updateDto.Amount,
            PaymentDate = updateDto.PaymentDate,
            PaymentMethod = updateDto.PaymentMethod
        };

        if (updateDto.Booking != null)
        {
            payment.BookingId = updateDto.Booking;
        }
        if (updateDto.CreatedAt != null)
        {
            payment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            payment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return payment;
    }
}
