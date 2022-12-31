using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=LAPTOP-9IQ5NO3T;Initial Catalog=ETicaret;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                opt => opt.EnableRetryOnFailure()
                )
                .EnableSensitiveDataLogging();

        }
    }
    public DbSet<Computer>  Computers{ get; set; }
    public DbSet<PriceByStore> PriceByStores { get; set; }

}
