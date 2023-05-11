using Domain;
using Domain.Service.IRepository;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbString = builder.Configuration.GetConnectionString("WebshopDB") ?? throw new InvalidOperationException("Connection string 'WebshopConnection' not found.");

builder.Services.AddDbContext<WebshopDbContext>(options =>
    options.UseMySQL(dbString));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();