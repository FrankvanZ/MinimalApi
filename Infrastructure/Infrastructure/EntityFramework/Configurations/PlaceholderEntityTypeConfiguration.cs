using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Infrastructure.EntityFramework.Configurations;

public class PlaceholderEntityTypeConfiguration : IEntityTypeConfiguration<Placeholder>
{
    public void Configure(EntityTypeBuilder<Placeholder> builder)
    {
        builder.Property(m => m.Id)
            .IsRequired();
    }
}