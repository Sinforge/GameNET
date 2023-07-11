using Microsoft.AspNetCore.SignalR;

namespace ChatService
{
    public class ChatHub : Hub
    {
        //Must be contains in db
        private readonly List<string> _groups = new() { "Dota 2", "CS:GO", "Dead By Daylight", "Fortnite", "Valorant", "League Of Legend" };
        public async Task AddToChat(string group, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Groups(group).SendAsync("Notify", $"User {userName} join to the chat");
        }

        public async Task SendMessage(string message, string userName, string group)
        {
            await Clients.Groups(group).SendAsync("Receive", message, userName);
        }
    }
}
