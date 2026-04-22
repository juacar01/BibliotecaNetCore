using Biblioteca.Api.Services;
using Biblioteca.Application;
using Biblioteca.Application.Features.Authors.Queries.GetAuthorList;
using Biblioteca.Application.Features.Books.Queries.GetBookList;
using Biblioteca.Infrastructure;
using Biblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped<IFileStorageService, PhysicalFileStorageService>();


// Add cadena de conexion .
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(BibliotecaDbContext).Assembly.FullName)));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAuthorListQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetBookListQueryHandler).Assembly);
}
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API .NET 10");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = scope.ServiceProvider.GetRequiredService<BibliotecaDbContext>();
        context.Database.Migrate();

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrio un error durante la migracion de la base de datos");
    }
}

app.Run();
