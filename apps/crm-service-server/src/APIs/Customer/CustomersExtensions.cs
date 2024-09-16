using CrmService.APIs.Dtos;
using CrmService.Infrastructure.Models;

namespace CrmService.APIs.Extensions;

public static class CustomersExtensions
{
    public static Customer ToDto(this CustomerDbModel model)
    {
        return new Customer
        {
            ContactInfo = model.ContactInfo,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Industry = model.Industry,
            Leads = model.Leads?.Select(x => x.Id).ToList(),
            Meetings = model.Meetings?.Select(x => x.Id).ToList(),
            Name = model.Name,
            Opportunities = model.Opportunities?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CustomerDbModel ToModel(
        this CustomerUpdateInput updateDto,
        CustomerWhereUniqueInput uniqueId
    )
    {
        var customer = new CustomerDbModel
        {
            Id = uniqueId.Id,
            ContactInfo = updateDto.ContactInfo,
            Industry = updateDto.Industry,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            customer.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            customer.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return customer;
    }
}
