using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CourseConfigurations : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Id,
                value => new CourseId(value)
            );
        builder
            .HasOne(c => c.Department)
            .WithMany(d => d.Courses);

        builder
            .HasOne(c => c.Teacher)
            .WithMany(t => t.Courses);

        builder
            .HasOne(c => c.Planning)
            .WithOne(p => p.Course)
            .HasForeignKey<Planning>(p => p.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
