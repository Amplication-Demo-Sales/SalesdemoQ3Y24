using CrmService.APIs.Dtos;
using CrmService.Infrastructure.Models;

namespace CrmService.APIs.Extensions;

public static class MeetingsExtensions
{
    public static Meeting ToDto(this MeetingDbModel model)
    {
        return new Meeting
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Date = model.Date,
            Id = model.Id,
            Lead = model.LeadId,
            Location = model.Location,
            Notes = model.Notes,
            Subject = model.Subject,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static MeetingDbModel ToModel(
        this MeetingUpdateInput updateDto,
        MeetingWhereUniqueInput uniqueId
    )
    {
        var meeting = new MeetingDbModel
        {
            Id = uniqueId.Id,
            Date = updateDto.Date,
            Location = updateDto.Location,
            Notes = updateDto.Notes,
            Subject = updateDto.Subject
        };

        if (updateDto.CreatedAt != null)
        {
            meeting.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            meeting.CustomerId = updateDto.Customer;
        }
        if (updateDto.Lead != null)
        {
            meeting.LeadId = updateDto.Lead;
        }
        if (updateDto.UpdatedAt != null)
        {
            meeting.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return meeting;
    }
}
