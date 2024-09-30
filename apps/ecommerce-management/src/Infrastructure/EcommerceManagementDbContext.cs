using EcommerceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManagement.Infrastructure;

public class EcommerceManagementDbContext : DbContext
{
    public EcommerceManagementDbContext(DbContextOptions<EcommerceManagementDbContext> options)
        : base(options) { }

    public DbSet<ArticleDbModel> Articles { get; set; }

    public DbSet<ProfileDbModel> Profiles { get; set; }

    public DbSet<CommentDbModel> Comments { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
