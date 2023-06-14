using System.Reflection;
using System.Text;
using ArticleService.Data;
using ArticleService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(options=>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var jwtConfig = builder.Configuration.GetSection("Audience");
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig["Secret"])),
            ValidateIssuer = true,
            ValidIssuer = jwtConfig["Iss"],
            ValidateAudience = true,
            ValidAudience = jwtConfig["Aud"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true,

        };
    });
builder.Services.AddAuthorization();


builder.Services.AddSingleton<ApplicationContext>();
builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddSingleton<IMessageProducer, MessageProducer>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Account}/{action=HelloWorld}");
app.Run();
