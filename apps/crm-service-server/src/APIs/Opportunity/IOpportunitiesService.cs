using CrmService.APIs.Common;
using CrmService.APIs.Dtos;

namespace CrmService.APIs;

public interface IOpportunitiesService
{
    /// <summary>
    /// Create one Opportunity
    /// </summary>
    public Task<Opportunity> CreateOpportunity(OpportunityCreateInput opportunity);

    /// <summary>
    /// Delete one Opportunity
    /// </summary>
    public Task DeleteOpportunity(OpportunityWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Opportunities
    /// </summary>
    public Task<List<Opportunity>> Opportunities(OpportunityFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Opportunity records
    /// </summary>
    public Task<MetadataDto> OpportunitiesMeta(OpportunityFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Opportunity
    /// </summary>
    public Task<Opportunity> Opportunity(OpportunityWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Opportunity
    /// </summary>
    public Task UpdateOpportunity(
        OpportunityWhereUniqueInput uniqueId,
        OpportunityUpdateInput updateDto
    );

    /// <summary>
    /// Get a Customer record for Opportunity
    /// </summary>
    public Task<Customer> GetCustomer(OpportunityWhereUniqueInput uniqueId);
}
