using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Infraestructure.Persistence.Context;
using TaskManagement.Infraestructure.Persistence.Repositories;

namespace TaskManagement.Infraestructure.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)),
            ServiceLifetime.Scoped);

        //Inyecciones de dependencias.
        services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddTransient<ITasksRepository, TasksRepository>();
    }
}