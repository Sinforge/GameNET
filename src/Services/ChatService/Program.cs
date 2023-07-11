using ChatService;
using ChatService.Data;
using ChatService.Extentions;
using ChatService.Services;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabase(builder.Configuration);
builder.Configuration.AddJsonFile("badWords.json");
builder.Services.AddControllers();
builder.Services.AddScoped<IChatService, ChatService.Services.ChatService>();
builder.Services.AddSignalR(options => options.AddFilter<BadWordsHubFilter>());      // ���������� ������� SignalR
builder.Services.AddScoped<IChatRepo, ChatRepo>();
var app = builder.Build();
app.CreateMigrations("chat");
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");   // ChatHub ����� ������������ ������� �� ���� /chat
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller=Chat}/{action=GetAllGames}");
app.Run();

