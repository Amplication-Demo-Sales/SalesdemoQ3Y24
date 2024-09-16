using BookingService.APIs.Dtos;
using BookingService.Infrastructure.Models;

namespace BookingService.APIs.Extensions;

public static class EventsExtensions
{
    public static Event ToDto(this EventDbModel model)
    {
        return new Event
        {
            Bookings = model.Bookings?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Date = model.Date,
            EventName = model.EventName,
            Id = model.Id,
            Location = model.Location,
            Notes = model.Notes,
            UpdatedAt = model.UpdatedAt,

        };
    }

    public static EventDbModel ToModel(this EventUpdateInput updateDto, EventWhereUniqueInput uniqueId)
    {
        var event = new EventDbModel { 
               Id = uniqueId.Id,
Date = updateDto.Date,
EventName = updateDto.EventName,
Location = updateDto.Location,
Notes = updateDto.Notes
};

     if(updateDto.CreatedAt != null) {
     event.CreatedAt = updateDto.CreatedAt.Value;
}
if(updateDto.UpdatedAt != null) {
     event.UpdatedAt = updateDto.UpdatedAt.Value;
}

    return event; }

}
