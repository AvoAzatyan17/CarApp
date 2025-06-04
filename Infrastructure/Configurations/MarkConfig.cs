using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MarkConfig: IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.Property(n => n.Name).IsRequired().HasMaxLength(30);
        builder.HasQueryFilter(n => !n.IsDeleted);
    }
}