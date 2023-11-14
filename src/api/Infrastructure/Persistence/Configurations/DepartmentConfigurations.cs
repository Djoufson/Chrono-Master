using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        builder.HasKey(d => d.Id);
        builder
            .Property(d => d.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Id,
                value => new DepartmentId(value)
            );

        builder
            .HasMany(d => d.Courses)
            .WithOne(c => c.Department);
    }
}
