using Scalar.AspNetCore;
using SmartBlog.Api.Middleware;
using SmartBlog.Application;
using SmartBlog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Registrera controllers, OpenAPI-dokumentation och tjänsterna från Application- och Infrastructure-lagren
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Aktivera OpenAPI och Scalar (API-dokumentation) bara i utvecklingsmiljö
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Registrera felhanteringsmiddleware först i pipelinen så den fångar alla exceptions
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
