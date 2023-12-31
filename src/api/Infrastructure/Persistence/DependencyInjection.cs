using Application.Persistence;
using Application.Persistence.Base;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration config,
        IHostEnvironment env)
    {
        services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlite("Data Source=Database/app.db"));
        // if(env.IsDevelopment())
        // {
        //     Console.WriteLine("---> Development");
        //     services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlite("Data Source=Database/app.db"));
        // }
        // else if(env.IsProduction())
        // {
        //     Console.WriteLine("---> Production");
        //     services.AddDbContext<AppDbContext>(cfg => cfg.UseNpgsql(config.GetConnectionString("Postgresql")));
        // }
        // else
        // {
        //     Console.WriteLine("---> Else");
        //     services.AddDbContext<AppDbContext>(cfg => cfg.UseInMemoryDatabase("Database"));
        // }

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<ICoursesRepository, CoursesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
