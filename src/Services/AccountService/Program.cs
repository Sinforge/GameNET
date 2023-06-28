using AccountService.Extentions;
using AccountService.IntegrationEvents.EventHandlers;
using AccountService.IntegrationEvents.Events;
using EventBus.Abstractions;
using Microsoft.Extensions.Options;
using Shared.Extentions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//Add services
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddTransient<UserCreateArticleIntegrationEventHandler>();
builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<UserCreateArticleIntegrationEvent, UserCreateArticleIntegrationEventHandler>();
// If in development add swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.CreateMigrations("accountdb");
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Account}/{action=HelloWorld}");
app.Run();



