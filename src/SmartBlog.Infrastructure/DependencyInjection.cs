using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartBlog.Application.Interfaces;
using SmartBlog.Infrastructure.Persistence;
using SmartBlog.Infrastructure.Repositories;
using SmartBlog.Infrastructure.Services;

namespace SmartBlog.Infrastructure;

// Extension-metod som registrerar alla tjänster i Infrastructure-lagret
// Anropas från Program.cs med builder.Services.AddInfrastructure(builder.Configuration)
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Registrera EF Core med SQL Server och hämta connection string från appsettings.json
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Registrera OpenAI-tjänsten med en konfigurerad HttpClient
        // AddHttpClient ger varje instans en egen HttpClient med rätt basadress och API-nyckel
        services.AddHttpClient<IOpenAiService, OpenAiService>(client =>
        {
            client.BaseAddress = new Uri("https://api.openai.com/v1/");
            client.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {configuration["OpenAi:ApiKey"]}");
        });

        // Registrera repository - kopplar ihop interfacet med implementationen
        // AddScoped = en ny instans per HTTP-request
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();

        return services;
    }
}
