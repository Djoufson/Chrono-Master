using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication Seed(this WebApplication app)
    {
        if(app.Environment.IsDevelopment())
        {
            Console.WriteLine("---> Seeding");
            using var scope = app.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if(ctx.Users.Any())
                return app;

            var user = Admin.CreateUnique(
                Name.Create("John", "Doe"),
                Password.Create("string")
            );

            var dpts = new []
            {
                Department.CreateUnique("Software Engineering", "SE", null),
                Department.CreateUnique("Artificial Intelligence", "AI", null),
                Department.CreateUnique("Accounting & Finance", "AF", null)
            };

            ctx.Add(user);
            ctx.AddRange(dpts);
            ctx.SaveChanges();
        }

        return app;
    }
}
