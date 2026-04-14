using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartBlog.Application.Behaviors;

namespace SmartBlog.Application;

// Extension-metod som registrerar alla tjänster i Application-lagret
// Anropas från Program.cs med builder.Services.AddApplication()
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrera alla MediatR handlers (commands/queries) från detta assembly
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Registrera alla FluentValidation-validators från detta assembly
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Registrera LoggingPipelineBehavior FÖRE ValidationBehavior
        // Ordningen spelar roll: den som registreras först körs ytterst i pipelinen
        // Logging wrappas runt allt, så vi fångar hela tiden inklusive validering
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        // Registrera ValidationBehavior som en pipeline behavior i MediatR
        // Den körs automatiskt innan varje handler och validerar inkommande requests
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Registrera alla AutoMapper-profiler från detta assembly
        services.AddAutoMapper(cfg =>
            cfg.AddMaps(typeof(DependencyInjection).Assembly));

        return services;
    }
}
