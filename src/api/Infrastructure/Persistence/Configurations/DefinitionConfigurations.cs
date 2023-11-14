using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DefinitionConfigurations : IEntityTypeConfiguration<Definition>
{
    public void Configure(EntityTypeBuilder<Definition> builder)
    {
        builder.ToTable("Definitions");
        builder.HasKey(d => d.Id);
        builder
            .Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Id,
                value => new DefinitionId(value)
            );

        builder
            .HasMany(d => d.Items)
            .WithOne(di => di.Definition)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
