using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PlanningConfigurations : IEntityTypeConfiguration<Planning>
{
    public void Configure(EntityTypeBuilder<Planning> builder)
    {
        builder.ToTable("Plannings");
        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Id)
            .HasConversion(
                id => id.Id,
                value => new PlanningId(value)
            );

        builder
            .HasOne(p => p.Course)
            .WithOne(c => c.Planning);

        builder
            .OwnsMany(p => p.Sessions, psb =>
        {
            psb.WithOwner();
            psb.ToTable("Sessions");
            psb.HasKey(s => s.Id);
            psb
                .Property(s => s.Id)
                .HasConversion(
                    id => id.Id,
                    value => new SessionId(value)
                );

            psb
                .HasOne(s => s.Planning)
                .WithMany(p => p.Sessions);
        });
    }
}
