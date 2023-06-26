
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.UseOcelot();
app.Run();
