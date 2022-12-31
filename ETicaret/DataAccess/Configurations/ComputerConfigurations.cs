using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ComputerConfigurations : IEntityTypeConfiguration<Computer>
{
    public void Configure(EntityTypeBuilder<Computer> builder)
    {
        builder.HasKey(s=>s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();
        builder.HasMany(s => s.PriceByStore);
        builder.Property(s => s.PriceByStore).IsRequired();
    }
}
