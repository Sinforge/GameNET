using System.Security.Claims;
using GameHub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("Cookies")
    .AddCookie();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString(name: "DefaultConnection")));

builder.Services.AddAuthorization();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseDeveloperExceptionPage();



app.MapGet("/registration/{userId}/{userName}/{password}/{email}",
    async (HttpContext context ,ApplicationContext db, string userId, string userName,string password, string email) =>
{
    User? user =db.Users.FirstOrDefault(u => u.UserId == userId || u.Email == email);
    if (user != null)
    {
        await context.Response.WriteAsync("User with same id or email is exist");
    }

    db.Add( new User
    {
        UserId = userId,
        Name = userName,
        Password = password,
        Email = email,
        Role = Role.DefaultUser
    });
    
    await db.SaveChangesAsync();
});

app.MapGet("/article", async (HttpContext ctx, ApplicationContext db) =>
{
    var request = ctx.Request;
    string message = "Bad request";
    ArticleViewModel? json;
    try
    {
        json = await request.ReadFromJsonAsync<ArticleViewModel>();

        if (json != null)
        {
            Article article = new Article()
            {
                Title = json.Title,
                Text = json.Text,
                Owner = await db.Users.FirstOrDefaultAsync(u => u.UserId == json.Owner)
            };
            db.Articles.Add(article);
            await db.SaveChangesAsync();
            message = "Successful request";
        }
    }
    catch (Exception ex)
    { 
        message = ex.Message;
    }

    await ctx.Response.WriteAsync(message);


});
app.MapGet("/login/{userId}/{password}",
    async (HttpContext ctx, ApplicationContext db, string userId, string password)=> 
    {
        User? user = db.Users.FirstOrDefault(u => u.UserId == userId && u.Password == password);
        if (user == null)
        {
            await ctx.Response.WriteAsync("Incorrect userid or password");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userId),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        await ctx.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
        await ctx.Response.WriteAsync("Successful login");

    });
app.MapGet("/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync("Cookies");
    await ctx.Response.WriteAsync("Successful logout");
});

app.MapGet("/", () => "Hello World!");

app.Run();
