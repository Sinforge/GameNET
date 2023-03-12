using AccountService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AccountService.Controllers.Audience>(builder.Configuration.GetSection("Audience"));

builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString(name: "DefaultConnection")));
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Account}/{action=HelloWorld}");

app.Run();



