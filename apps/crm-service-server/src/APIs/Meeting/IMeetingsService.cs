using CrmService.APIs.Common;
using CrmService.APIs.Dtos;

namespace CrmService.APIs;

public interface IMeetingsService
{
    /// <summary>
    /// Create one Meeting
    /// </summary>
    public Task<Meeting> CreateMeeting(MeetingCreateInput meeting);

    /// <summary>
    /// Delete one Meeting
    /// </summary>
    public Task DeleteMeeting(MeetingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Meetings
    /// </summary>
    public Task<List<Meeting>> Meetings(MeetingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Meeting records
    /// </summary>
    public Task<MetadataDto> MeetingsMeta(MeetingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Meeting
    /// </summary>
    public Task<Meeting> Meeting(MeetingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Meeting
    /// </summary>
    public Task UpdateMeeting(MeetingWhereUniqueInput uniqueId, MeetingUpdateInput updateDto);

    /// <summary>
    /// Get a Customer record for Meeting
    /// </summary>
    public Task<Customer> GetCustomer(MeetingWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Lead record for Meeting
    /// </summary>
    public Task<Lead> GetLead(MeetingWhereUniqueInput uniqueId);
}
