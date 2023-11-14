using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DefinitionItemConfigurations : IEntityTypeConfiguration<DefinitionItem>
{
    public void Configure(EntityTypeBuilder<DefinitionItem> builder)
    {
        builder.ToTable("DefinitionItems");
        builder.HasKey(di => di.Id);
        builder
            .Property(di => di.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Id,
                value => new DefinitionItemId(value)
            );
    }
}
