using CrmService.APIs;
using CrmService.APIs.Common;
using CrmService.APIs.Dtos;
using CrmService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Leads records to Customer
    /// </summary>
    [HttpPost("{Id}/leads")]
    public async Task<ActionResult> ConnectLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] LeadWhereUniqueInput[] leadsId
    )
    {
        try
        {
            await _service.ConnectLeads(uniqueId, leadsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Leads records from Customer
    /// </summary>
    [HttpDelete("{Id}/leads")]
    public async Task<ActionResult> DisconnectLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] LeadWhereUniqueInput[] leadsId
    )
    {
        try
        {
            await _service.DisconnectLeads(uniqueId, leadsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Leads records for Customer
    /// </summary>
    [HttpGet("{Id}/leads")]
    public async Task<ActionResult<List<Lead>>> FindLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] LeadFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindLeads(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Leads records for Customer
    /// </summary>
    [HttpPatch("{Id}/leads")]
    public async Task<ActionResult> UpdateLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] LeadWhereUniqueInput[] leadsId
    )
    {
        try
        {
            await _service.UpdateLeads(uniqueId, leadsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Meetings records to Customer
    /// </summary>
    [HttpPost("{Id}/meetings")]
    public async Task<ActionResult> ConnectMeetings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] MeetingWhereUniqueInput[] meetingsId
    )
    {
        try
        {
            await _service.ConnectMeetings(uniqueId, meetingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Meetings records from Customer
    /// </summary>
    [HttpDelete("{Id}/meetings")]
    public async Task<ActionResult> DisconnectMeetings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] MeetingWhereUniqueInput[] meetingsId
    )
    {
        try
        {
            await _service.DisconnectMeetings(uniqueId, meetingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Meetings records for Customer
    /// </summary>
    [HttpGet("{Id}/meetings")]
    public async Task<ActionResult<List<Meeting>>> FindMeetings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] MeetingFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindMeetings(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Meetings records for Customer
    /// </summary>
    [HttpPatch("{Id}/meetings")]
    public async Task<ActionResult> UpdateMeetings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] MeetingWhereUniqueInput[] meetingsId
    )
    {
        try
        {
            await _service.UpdateMeetings(uniqueId, meetingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Opportunities records to Customer
    /// </summary>
    [HttpPost("{Id}/opportunities")]
    public async Task<ActionResult> ConnectOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OpportunityWhereUniqueInput[] opportunitiesId
    )
    {
        try
        {
            await _service.ConnectOpportunities(uniqueId, opportunitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Opportunities records from Customer
    /// </summary>
    [HttpDelete("{Id}/opportunities")]
    public async Task<ActionResult> DisconnectOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OpportunityWhereUniqueInput[] opportunitiesId
    )
    {
        try
        {
            await _service.DisconnectOpportunities(uniqueId, opportunitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Opportunities records for Customer
    /// </summary>
    [HttpGet("{Id}/opportunities")]
    public async Task<ActionResult<List<Opportunity>>> FindOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OpportunityFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindOpportunities(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Opportunities records for Customer
    /// </summary>
    [HttpPatch("{Id}/opportunities")]
    public async Task<ActionResult> UpdateOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OpportunityWhereUniqueInput[] opportunitiesId
    )
    {
        try
        {
            await _service.UpdateOpportunities(uniqueId, opportunitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("customers")]
    public async Task<Customer> ListCustomers(
        [FromBody()] CustomerFindManyArgs customerFindManyArgsDto
    )
    {
        return await _service.ListCustomers(customerFindManyArgsDto);
    }
}
