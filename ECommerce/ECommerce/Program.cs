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

var app = builder.Build(); // After this line everthing is middleware sushil

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run(); // This can execute the program class sushil
