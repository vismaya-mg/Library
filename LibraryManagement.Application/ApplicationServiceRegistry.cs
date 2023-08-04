using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryManagement.Application;

/// <summary>
/// Class that registers all interfaces and implementations to DI
/// </summary>
public static class ApplicationServiceRegistry
{
    /// <summary>
    /// Adds the Application services.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        //Adds MediatR to Di from the Execution Assembly 
        services.AddMediatR(mediator => mediator.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //Adds all the validators to the Di from the execution assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}