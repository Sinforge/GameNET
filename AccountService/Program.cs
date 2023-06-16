using AccountService.Infrastructure.Extentions.ServiceCollection;
using Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);
//Add services
builder.Services.AddSwagger();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationServices();

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



