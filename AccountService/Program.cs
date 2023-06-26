using AccountService.Extentions;
using Microsoft.Extensions.Options;
using Shared.Extentions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//Add services
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();

var app = builder.Build();
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



