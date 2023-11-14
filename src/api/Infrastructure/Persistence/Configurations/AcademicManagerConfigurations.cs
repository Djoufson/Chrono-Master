using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AcademicManagerConfigurations : IEntityTypeConfiguration<AcademicManager>
{
    public void Configure(EntityTypeBuilder<AcademicManager> builder)
    {
        builder
            .HasOne(a => a.Department)
            .WithOne(d => d.Manager)
            .HasForeignKey<AcademicManager>(a => a.DepartmentId);
    }
}
