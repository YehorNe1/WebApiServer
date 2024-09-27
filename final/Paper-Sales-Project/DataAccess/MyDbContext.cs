using Microsoft.EntityFrameworkCore;
using Paper_Project.Models;


namespace Paper_Project.DataAccess;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Paper> Papers { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<OrderEntry> OrderEntries { get; set; }
}