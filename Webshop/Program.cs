using Domain;
using Domain.Service.IRepository;
using Infrastructure;
using Infrastructure.RabbitMQ;
using Infrastructure.MySQL;
using Infrastructure.Mongo;
using Microsoft.EntityFrameworkCore;
using Webshop.Commands;
using Webshop.Consumer;
using Webshop.Dto;
using Webshop.Handlers;
using Webshop.Interfaces;
using Webshop.Queries;


var builder = WebApplication.CreateBuilder(args);

// database
var dbString = builder.Configuration.GetConnectionString("WebshopDB") ?? throw new InvalidOperationException("Connection string 'WebshopConnection' not found.");
builder.Services.AddDbContext<WebshopDbContext>(options =>
    options.UseMySQL(dbString));

builder.Services.Configure<WebshopDatabaseSettings>(
    builder.Configuration.GetSection("Mongo"));

// repository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

//command handler
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderQuery, OrderDocument>, GetOrderQueryHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// background service
builder.Services.AddHostedService<RabbitMQConsumer>();


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