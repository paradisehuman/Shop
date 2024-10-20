using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts;
using Shop.Application.Services;
using Shop.Domain.Contracts;
using Shop.Domain.Events.Customer;
using Shop.Infrastructure;
using Shop.Infrastructure.Contracts;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Options;
using Shop.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDomainEventDispatcher, RabbitMqDomainEventDispatcher>();

builder.Services.AddScoped<ICustomerService, CustomerService>()
    .AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(Program).Assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        x.AddConsumer<CustomerCreatedEventHandler>();
        
        var rabbitMqOptions = builder.Configuration.GetSection("RabbitMqOptions").Get<RabbitMqOptions>();
        
        cfg.DefaultContentType = new ContentType("application/json");

        cfg.UseRawJsonSerializer();

        cfg.ConfigureJsonSerializerOptions(options =>
        {
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return options;
        });
        cfg.Host(rabbitMqOptions?.Host, rabbitMqOptions!.Port, rabbitMqOptions.VirtualHost, h =>
        {
            h.Username(rabbitMqOptions.UserName);
            h.Password(rabbitMqOptions.Password);
        });
        
        cfg.ReceiveEndpoint("customer-created-event-queue", ep =>
        {
            ep.Durable = true;
            ep.ConfigureConsumer<CustomerCreatedEventHandler>(context);
            ep.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });
        
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();