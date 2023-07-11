using ChatService.Models;

namespace ChatService.Services
{
    public interface IChatService
    {
        Task<IEnumerable<GameInfo>> GetAllGamesAsync();
        Task<IEnumerable<ChatInfo>> GetAllChatsByGameNameAsync(string name);
        Task CreateGameAsync(GameInfo gameInfo);
        Task CreateChatAsync(ChatInfo chatInfo);
        Task<bool> RemoveChatAsync(Guid chatId);
    }
}
