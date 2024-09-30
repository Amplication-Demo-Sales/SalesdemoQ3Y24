using BlogManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure;

public class BlogManagementDbContext : IdentityDbContext<IdentityUser>
{
    public BlogManagementDbContext(DbContextOptions<BlogManagementDbContext> options)
        : base(options) { }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<ProductDbModel> Products { get; set; }

    public DbSet<CategoryDbModel> Categories { get; set; }
}
