using Domain.Entities.Base;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(Guid.Parse(value))
            );

        builder
            .Property(u => u.Password)
            .HasConversion(
                pwd => pwd.Value,
                value => Password.Create(value)
            );

        builder.OwnsOne(u => u.Name, name =>
        {
            name
                .Property(n => n.FirstName)
                .HasColumnName("FirstName");
            name
                .Property(n => n.LastName)
                .HasColumnName("LastName");
        });

        builder
            .Property(u => u.Role);
    }
}
