using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TaskManagement.Core.Application;

/// <summary>
/// Static class for registering application layer services.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Registers application layer services such as AutoMapper and MediatR.
    /// </summary>
    /// <param name="services">The collection of services to add to.</param>
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        // Register AutoMapper to map between objects
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register MediatR for implementing the mediator pattern
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly()));
    }
}