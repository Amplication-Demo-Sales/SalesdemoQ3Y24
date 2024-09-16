using CrmService.APIs;
using CrmService.APIs.Common;
using CrmService.APIs.Dtos;
using CrmService.APIs.Errors;
using CrmService.APIs.Extensions;
using CrmService.Infrastructure;
using CrmService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmService.APIs;

public abstract class MeetingsServiceBase : IMeetingsService
{
    protected readonly CrmServiceDbContext _context;

    public MeetingsServiceBase(CrmServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Meeting
    /// </summary>
    public async Task<Meeting> CreateMeeting(MeetingCreateInput createDto)
    {
        var meeting = new MeetingDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Date = createDto.Date,
            Location = createDto.Location,
            Notes = createDto.Notes,
            Subject = createDto.Subject,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            meeting.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            meeting.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Lead != null)
        {
            meeting.Lead = await _context
                .Leads.Where(lead => createDto.Lead.Id == lead.Id)
                .FirstOrDefaultAsync();
        }

        _context.Meetings.Add(meeting);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<MeetingDbModel>(meeting.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Meeting
    /// </summary>
    public async Task DeleteMeeting(MeetingWhereUniqueInput uniqueId)
    {
        var meeting = await _context.Meetings.FindAsync(uniqueId.Id);
        if (meeting == null)
        {
            throw new NotFoundException();
        }

        _context.Meetings.Remove(meeting);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Meetings
    /// </summary>
    public async Task<List<Meeting>> Meetings(MeetingFindManyArgs findManyArgs)
    {
        var meetings = await _context
            .Meetings.Include(x => x.Lead)
            .Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return meetings.ConvertAll(meeting => meeting.ToDto());
    }

    /// <summary>
    /// Meta data about Meeting records
    /// </summary>
    public async Task<MetadataDto> MeetingsMeta(MeetingFindManyArgs findManyArgs)
    {
        var count = await _context.Meetings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Meeting
    /// </summary>
    public async Task<Meeting> Meeting(MeetingWhereUniqueInput uniqueId)
    {
        var meetings = await this.Meetings(
            new MeetingFindManyArgs { Where = new MeetingWhereInput { Id = uniqueId.Id } }
        );
        var meeting = meetings.FirstOrDefault();
        if (meeting == null)
        {
            throw new NotFoundException();
        }

        return meeting;
    }

    /// <summary>
    /// Update one Meeting
    /// </summary>
    public async Task UpdateMeeting(MeetingWhereUniqueInput uniqueId, MeetingUpdateInput updateDto)
    {
        var meeting = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            meeting.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Lead != null)
        {
            meeting.Lead = await _context
                .Leads.Where(lead => updateDto.Lead == lead.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(meeting).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Meetings.Any(e => e.Id == meeting.Id))
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
    /// Get a Customer record for Meeting
    /// </summary>
    public async Task<Customer> GetCustomer(MeetingWhereUniqueInput uniqueId)
    {
        var meeting = await _context
            .Meetings.Where(meeting => meeting.Id == uniqueId.Id)
            .Include(meeting => meeting.Customer)
            .FirstOrDefaultAsync();
        if (meeting == null)
        {
            throw new NotFoundException();
        }
        return meeting.Customer.ToDto();
    }

    /// <summary>
    /// Get a Lead record for Meeting
    /// </summary>
    public async Task<Lead> GetLead(MeetingWhereUniqueInput uniqueId)
    {
        var meeting = await _context
            .Meetings.Where(meeting => meeting.Id == uniqueId.Id)
            .Include(meeting => meeting.Lead)
            .FirstOrDefaultAsync();
        if (meeting == null)
        {
            throw new NotFoundException();
        }
        return meeting.Lead.ToDto();
    }
}
