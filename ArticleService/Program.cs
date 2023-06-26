using ArticleService.Data;
using ArticleService.Extentions;
using ArticleService.Services;
using Shared.Extentions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwagger(builder.Configuration); 
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
app.CreateMigrations("article");
                      
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Account}/{action=HelloWorld}");
app.Run();
