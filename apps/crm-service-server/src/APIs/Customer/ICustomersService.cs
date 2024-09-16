using CrmService.APIs.Common;
using CrmService.APIs.Dtos;

namespace CrmService.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Leads records to Customer
    /// </summary>
    public Task ConnectLeads(CustomerWhereUniqueInput uniqueId, LeadWhereUniqueInput[] leadsId);

    /// <summary>
    /// Disconnect multiple Leads records from Customer
    /// </summary>
    public Task DisconnectLeads(CustomerWhereUniqueInput uniqueId, LeadWhereUniqueInput[] leadsId);

    /// <summary>
    /// Find multiple Leads records for Customer
    /// </summary>
    public Task<List<Lead>> FindLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadFindManyArgs LeadFindManyArgs
    );

    /// <summary>
    /// Update multiple Leads records for Customer
    /// </summary>
    public Task UpdateLeads(CustomerWhereUniqueInput uniqueId, LeadWhereUniqueInput[] leadsId);

    /// <summary>
    /// Connect multiple Meetings records to Customer
    /// </summary>
    public Task ConnectMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] meetingsId
    );

    /// <summary>
    /// Disconnect multiple Meetings records from Customer
    /// </summary>
    public Task DisconnectMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] meetingsId
    );

    /// <summary>
    /// Find multiple Meetings records for Customer
    /// </summary>
    public Task<List<Meeting>> FindMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingFindManyArgs MeetingFindManyArgs
    );

    /// <summary>
    /// Update multiple Meetings records for Customer
    /// </summary>
    public Task UpdateMeetings(
        CustomerWhereUniqueInput uniqueId,
        MeetingWhereUniqueInput[] meetingsId
    );

    /// <summary>
    /// Connect multiple Opportunities records to Customer
    /// </summary>
    public Task ConnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] opportunitiesId
    );

    /// <summary>
    /// Disconnect multiple Opportunities records from Customer
    /// </summary>
    public Task DisconnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] opportunitiesId
    );

    /// <summary>
    /// Find multiple Opportunities records for Customer
    /// </summary>
    public Task<List<Opportunity>> FindOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityFindManyArgs OpportunityFindManyArgs
    );

    /// <summary>
    /// Update multiple Opportunities records for Customer
    /// </summary>
    public Task UpdateOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] opportunitiesId
    );
    public Task<Customer> ListCustomers(CustomerFindManyArgs customerFindManyArgsDto);
}
