using CrmService.APIs.Common;
using CrmService.APIs.Dtos;

namespace CrmService.APIs;

public interface ILeadsService
{
    /// <summary>
    /// Create one Lead
    /// </summary>
    public Task<Lead> CreateLead(LeadCreateInput lead);

    /// <summary>
    /// Delete one Lead
    /// </summary>
    public Task DeleteLead(LeadWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Leads
    /// </summary>
    public Task<List<Lead>> Leads(LeadFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Lead records
    /// </summary>
    public Task<MetadataDto> LeadsMeta(LeadFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Lead
    /// </summary>
    public Task<Lead> Lead(LeadWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Lead
    /// </summary>
    public Task UpdateLead(LeadWhereUniqueInput uniqueId, LeadUpdateInput updateDto);

    /// <summary>
    /// Get a Customer record for Lead
    /// </summary>
    public Task<Customer> GetCustomer(LeadWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Meetings records to Lead
    /// </summary>
    public Task ConnectMeetings(
        LeadWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] meetingsId
    );

    /// <summary>
    /// Disconnect multiple Meetings records from Lead
    /// </summary>
    public Task DisconnectMeetings(
        LeadWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] meetingsId
    );

    /// <summary>
    /// Find multiple Meetings records for Lead
    /// </summary>
    public Task<List<Meeting>> FindMeetings(
        LeadWhereUniqueInput uniqueId,
        MeetingFindManyArgs MeetingFindManyArgs
    );

    /// <summary>
    /// Update multiple Meetings records for Lead
    /// </summary>
    public Task UpdateMeetings(LeadWhereUniqueInput uniqueId, MeetingWhereUniqueInput[] meetingsId);
}
