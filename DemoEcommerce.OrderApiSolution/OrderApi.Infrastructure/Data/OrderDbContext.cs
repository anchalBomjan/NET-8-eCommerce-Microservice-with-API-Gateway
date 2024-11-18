using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;
namespace OrderApi.Infrastructure.Data
{

    public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultSchema("Orders");
        //    base.OnModelCreating(modelBuilder);
        //}
    }

}
