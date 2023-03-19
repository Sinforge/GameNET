
using ChatService;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("badWords.json");


builder.Services.AddSignalR(options => options.AddFilter<BadWordsHubFilter>());      // подключема сервисы SignalR

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");   // ChatHub будет обрабатывать запросы по пути /chat

app.Run();

