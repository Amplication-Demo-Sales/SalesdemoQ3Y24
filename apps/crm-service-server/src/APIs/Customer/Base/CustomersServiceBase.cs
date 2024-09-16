using CrmService.APIs;
using CrmService.APIs.Common;
using CrmService.APIs.Dtos;
using CrmService.APIs.Errors;
using CrmService.APIs.Extensions;
using CrmService.Infrastructure;
using CrmService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmService.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly CrmServiceDbContext _context;

    public CustomersServiceBase(CrmServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            ContactInfo = createDto.ContactInfo,
            CreatedAt = createDto.CreatedAt,
            Industry = createDto.Industry,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Leads != null)
        {
            customer.Leads = await _context
                .Leads.Where(lead => createDto.Leads.Select(t => t.Id).Contains(lead.Id))
                .ToListAsync();
        }

        if (createDto.Meetings != null)
        {
            customer.Meetings = await _context
                .Meetings.Where(meeting =>
                    createDto.Meetings.Select(t => t.Id).Contains(meeting.Id)
                )
                .ToListAsync();
        }

        if (createDto.Opportunities != null)
        {
            customer.Opportunities = await _context
                .Opportunities.Where(opportunity =>
                    createDto.Opportunities.Select(t => t.Id).Contains(opportunity.Id)
                )
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerDbModel>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
    {
        var customer = await _context.Customers.FindAsync(uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Leads)
            .Include(x => x.Meetings)
            .Include(x => x.Opportunities)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
    {
        var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(
        CustomerWhereUniqueInput uniqueId,
        CustomerUpdateInput updateDto
    )
    {
        var customer = updateDto.ToModel(uniqueId);

        if (updateDto.Leads != null)
        {
            customer.Leads = await _context
                .Leads.Where(lead => updateDto.Leads.Select(t => t).Contains(lead.Id))
                .ToListAsync();
        }

        if (updateDto.Meetings != null)
        {
            customer.Meetings = await _context
                .Meetings.Where(meeting => updateDto.Meetings.Select(t => t).Contains(meeting.Id))
                .ToListAsync();
        }

        if (updateDto.Opportunities != null)
        {
            customer.Opportunities = await _context
                .Opportunities.Where(opportunity =>
                    updateDto.Opportunities.Select(t => t).Contains(opportunity.Id)
                )
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
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
    /// Connect multiple Leads records to Customer
    /// </summary>
    public async Task ConnectLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Leads)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Leads.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Leads);

        foreach (var child in childrenToConnect)
        {
            parent.Leads.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Leads records from Customer
    /// </summary>
    public async Task DisconnectLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Leads)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Leads.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Leads?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Leads records for Customer
    /// </summary>
    public async Task<List<Lead>> FindLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadFindManyArgs customerFindManyArgs
    )
    {
        var leads = await _context
            .Leads.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return leads.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Leads records for Customer
    /// </summary>
    public async Task UpdateLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Leads)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Leads.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Leads = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Meetings records to Customer
    /// </summary>
    public async Task ConnectMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Meetings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Meetings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Meetings);

        foreach (var child in childrenToConnect)
        {
            parent.Meetings.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Meetings records from Customer
    /// </summary>
    public async Task DisconnectMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Meetings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Meetings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Meetings?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Meetings records for Customer
    /// </summary>
    public async Task<List<Meeting>> FindMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingFindManyArgs customerFindManyArgs
    )
    {
        var meetings = await _context
            .Meetings.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return meetings.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Meetings records for Customer
    /// </summary>
    public async Task UpdateMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Meetings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Meetings.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Meetings = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Opportunities records to Customer
    /// </summary>
    public async Task ConnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Opportunities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Opportunities.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Opportunities);

        foreach (var child in childrenToConnect)
        {
            parent.Opportunities.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Opportunities records from Customer
    /// </summary>
    public async Task DisconnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Opportunities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Opportunities.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Opportunities?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Opportunities records for Customer
    /// </summary>
    public async Task<List<Opportunity>> FindOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityFindManyArgs customerFindManyArgs
    )
    {
        var opportunities = await _context
            .Opportunities.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return opportunities.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Opportunities records for Customer
    /// </summary>
    public async Task UpdateOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Opportunities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Opportunities.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Opportunities = children;
        await _context.SaveChangesAsync();
    }

    public async Task<Customer> ListCustomers(CustomerFindManyArgs customerFindManyArgsDto)
    {
        throw new NotImplementedException();
    }
}
