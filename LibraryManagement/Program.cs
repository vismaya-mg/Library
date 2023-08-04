using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagement.Application;
using LibraryManagement.Infrastructure;
using LibraryManagement.Persistence.Context;
using LibraryManagement.Services.Implementations;
using LibraryManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddApplicationServices();
    services.AddInfrastructureServices(builder.Configuration);
    services.AddSwaggerGen();
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddScoped<IBookService, BookService>();
    services.AddTransient<ILibraryDbContext, LibraryDbContext>();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Library Management",
            Description = "Api to perform library management"
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
    services.AddFluentValidationAutoValidation();
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}
