using CrmService.APIs;
using CrmService.APIs.Common;
using CrmService.APIs.Dtos;
using CrmService.APIs.Errors;
using CrmService.APIs.Extensions;
using CrmService.Infrastructure;
using CrmService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmService.APIs;

public abstract class LeadsServiceBase : ILeadsService
{
    protected readonly CrmServiceDbContext _context;

    public LeadsServiceBase(CrmServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Lead
    /// </summary>
    public async Task<Lead> CreateLead(LeadCreateInput createDto)
    {
        var lead = new LeadDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Priority = createDto.Priority,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            lead.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            lead.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Meetings != null)
        {
            lead.Meetings = await _context
                .Meetings.Where(meeting =>
                    createDto.Meetings.Select(t => t.Id).Contains(meeting.Id)
                )
                .ToListAsync();
        }

        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<LeadDbModel>(lead.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Lead
    /// </summary>
    public async Task DeleteLead(LeadWhereUniqueInput uniqueId)
    {
        var lead = await _context.Leads.FindAsync(uniqueId.Id);
        if (lead == null)
        {
            throw new NotFoundException();
        }

        _context.Leads.Remove(lead);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Leads
    /// </summary>
    public async Task<List<Lead>> Leads(LeadFindManyArgs findManyArgs)
    {
        var leads = await _context
            .Leads.Include(x => x.Customer)
            .Include(x => x.Meetings)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return leads.ConvertAll(lead => lead.ToDto());
    }

    /// <summary>
    /// Meta data about Lead records
    /// </summary>
    public async Task<MetadataDto> LeadsMeta(LeadFindManyArgs findManyArgs)
    {
        var count = await _context.Leads.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Lead
    /// </summary>
    public async Task<Lead> Lead(LeadWhereUniqueInput uniqueId)
    {
        var leads = await this.Leads(
            new LeadFindManyArgs { Where = new LeadWhereInput { Id = uniqueId.Id } }
        );
        var lead = leads.FirstOrDefault();
        if (lead == null)
        {
            throw new NotFoundException();
        }

        return lead;
    }

    /// <summary>
    /// Update one Lead
    /// </summary>
    public async Task UpdateLead(LeadWhereUniqueInput uniqueId, LeadUpdateInput updateDto)
    {
        var lead = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            lead.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Meetings != null)
        {
            lead.Meetings = await _context
                .Meetings.Where(meeting => updateDto.Meetings.Select(t => t).Contains(meeting.Id))
                .ToListAsync();
        }

        _context.Entry(lead).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Leads.Any(e => e.Id == lead.Id))
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
    /// Get a Customer record for Lead
    /// </summary>
    public async Task<Customer> GetCustomer(LeadWhereUniqueInput uniqueId)
    {
        var lead = await _context
            .Leads.Where(lead => lead.Id == uniqueId.Id)
            .Include(lead => lead.Customer)
            .FirstOrDefaultAsync();
        if (lead == null)
        {
            throw new NotFoundException();
        }
        return lead.Customer.ToDto();
    }

    /// <summary>
    /// Connect multiple Meetings records to Lead
    /// </summary>
    public async Task ConnectMeetings(
        LeadWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Leads.Include(x => x.Meetings)
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
    /// Disconnect multiple Meetings records from Lead
    /// </summary>
    public async Task DisconnectMeetings(
        LeadWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Leads.Include(x => x.Meetings)
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
    /// Find multiple Meetings records for Lead
    /// </summary>
    public async Task<List<Meeting>> FindMeetings(
        LeadWhereUniqueInput uniqueId,
        MeetingFindManyArgs leadFindManyArgs
    )
    {
        var meetings = await _context
            .Meetings.Where(m => m.LeadId == uniqueId.Id)
            .ApplyWhere(leadFindManyArgs.Where)
            .ApplySkip(leadFindManyArgs.Skip)
            .ApplyTake(leadFindManyArgs.Take)
            .ApplyOrderBy(leadFindManyArgs.SortBy)
            .ToListAsync();

        return meetings.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Meetings records for Lead
    /// </summary>
    public async Task UpdateMeetings(
        LeadWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] childrenIds
    )
    {
        var lead = await _context
            .Leads.Include(t => t.Meetings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (lead == null)
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

        lead.Meetings = children;
        await _context.SaveChangesAsync();
    }
}
