using BookingService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure;

public class BookingServiceDbContext : DbContext
{
    public BookingServiceDbContext(DbContextOptions<BookingServiceDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<BookingDbModel> Bookings { get; set; }

    public DbSet<RoomDbModel> Rooms { get; set; }

    public DbSet<EventDbModel> Events { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }
}
