using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CarConfig: IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(n => n.Name).IsRequired().HasMaxLength(30);
        builder.Property(n => n.MakeId).IsRequired();
        builder.HasQueryFilter(n => !n.IsDeleted);
    }
}