using CrmService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmService.Infrastructure;

public class CrmServiceDbContext : DbContext
{
    public CrmServiceDbContext(DbContextOptions<CrmServiceDbContext> options)
        : base(options) { }

    public DbSet<LeadDbModel> Leads { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<MeetingDbModel> Meetings { get; set; }

    public DbSet<OpportunityDbModel> Opportunities { get; set; }
}
