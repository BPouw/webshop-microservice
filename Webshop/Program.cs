using Domain;
using Domain.Service.IRepository;
using Domain.Service.IService;
using Domain.Service.Service;
using Infrastructure;
using Infrastructure.RabbitMQ;
using Infrastructure.MySQL;
using Infrastructure.Mongo;
using Microsoft.EntityFrameworkCore;
using Webshop.Commands;
using Webshop.Consumers;
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

// service
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

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
builder.Services.AddHostedService<OrderConsumer>();
builder.Services.AddHostedService<StockConsumer>();

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WebshopDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.MapGet("/", () => "Hello World!");

app.Run();