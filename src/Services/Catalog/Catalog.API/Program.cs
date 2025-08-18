var builder = WebApplication.CreateBuilder(args);

// Add services to Container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString(name: "Database")!);

}).UseLightweightSessions() ;

var app = builder.Build();

// Configure the Http Request pipeline
app.MapCarter();

app.Run();
