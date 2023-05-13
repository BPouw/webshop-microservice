using Domain;
using Domain.Service.IRepository;
using Infrastructure;
using Infrastructure.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Webshop.Commands;
using Webshop.Handlers;
using Webshop.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var dbString = builder.Configuration.GetConnectionString("WebshopDB") ?? throw new InvalidOperationException("Connection string 'WebshopConnection' not found.");

builder.Services.AddControllers();

builder.Services.AddDbContext<WebshopDbContext>(options =>
    options.UseMySQL(dbString));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () => "Hello World!");

app.Run();