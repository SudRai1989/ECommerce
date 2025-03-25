using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // Afetr this line everything service sushil

// Add services to the container.

builder.Services.AddControllers();

// This use for DB Connection service using EntityFramework
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// This service added for creating service for ProductRepository is a implementation class IProductRepository is a interface
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add service for Generic Repository
builder .Services .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build(); // After this line everthing is middleware sushil

// Configure the HTTP request pipeline.

app.MapControllers();

// This try block is using for the Create service for SeedContextData and seeding data 
// If we declare service outside the dependecy injection then we need to CreateScope()
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync(); // This line similar to Update-Database 
    await StoreContextSeed.SeedAsync(context);
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}

app.Run(); // This can execute the program class sushil
