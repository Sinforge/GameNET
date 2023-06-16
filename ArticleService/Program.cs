using System.Reflection;
using System.Text;
using ArticleService.Data;
using ArticleService.Services;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwagger();
builder.Services.AddAuth(builder.Configuration);

//extract to extention

builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

builder.Services.AddDatabase(builder.Configuration);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.CreateMigrations("articledb");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Account}/{action=HelloWorld}");
app.Run();
