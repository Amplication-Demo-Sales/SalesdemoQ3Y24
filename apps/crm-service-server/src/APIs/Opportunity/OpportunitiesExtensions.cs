using CrmService.APIs.Dtos;
using CrmService.Infrastructure.Models;

namespace CrmService.APIs.Extensions;

public static class OpportunitiesExtensions
{
    public static Opportunity ToDto(this OpportunityDbModel model)
    {
        return new Opportunity
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Description = model.Description,
            Id = model.Id,
            PotentialRevenue = model.PotentialRevenue,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OpportunityDbModel ToModel(
        this OpportunityUpdateInput updateDto,
        OpportunityWhereUniqueInput uniqueId
    )
    {
        var opportunity = new OpportunityDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            PotentialRevenue = updateDto.PotentialRevenue,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            opportunity.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            opportunity.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            opportunity.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return opportunity;
    }
}
