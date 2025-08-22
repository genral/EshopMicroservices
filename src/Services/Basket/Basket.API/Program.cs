

using Basket.API.Data;
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


// Add Application Services
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
 
//Data Services
builder.Services.AddMarten(options =>

{
    options.Connection(builder.Configuration.GetConnectionString(name: "Database")!);

}).UseLightweightSessions();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CashedBasketRepository>();
 
//GRPC Servcies
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!); 
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback=HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});

//Corss-cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>(); 
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
