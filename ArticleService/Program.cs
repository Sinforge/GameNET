using System.Security.Claims;
using GameHub.Data;
using GameHub.DTOs;
using GameHub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("Cookies")
    .AddCookie();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString(name: "DefaultConnection")));
builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseDeveloperExceptionPage();






app.Run();
