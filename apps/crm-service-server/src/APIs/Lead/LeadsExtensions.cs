using CrmService.APIs.Dtos;
using CrmService.Infrastructure.Models;

namespace CrmService.APIs.Extensions;

public static class LeadsExtensions
{
    public static Lead ToDto(this LeadDbModel model)
    {
        return new Lead
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            Meetings = model.Meetings?.Select(x => x.Id).ToList(),
            Name = model.Name,
            Priority = model.Priority,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static LeadDbModel ToModel(this LeadUpdateInput updateDto, LeadWhereUniqueInput uniqueId)
    {
        var lead = new LeadDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Priority = updateDto.Priority,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            lead.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            lead.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            lead.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return lead;
    }
}
