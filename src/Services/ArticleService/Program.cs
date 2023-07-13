using ArticleService.Data;
using ArticleService.Extentions;
using ArticleService.IntegrationEvents;
using ArticleService.Services;
using Shared.Extentions;
var _myAllowSpecificOrigins = "MyAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwagger(builder.Configuration); 
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200");
        });
});
//extract to extention

builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddTransient<IArticleIntegrationEventService, ArticleIntegrationEventService>();

builder.Services.AddDatabase(builder.Configuration);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(_myAllowSpecificOrigins);
app.CreateMigrations("article");
                      
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Account}/{action=HelloWorld}");
app.Run();
