
using ChatService.Models;

namespace ChatService.Data
{
    public interface IChatRepo
    {
        Task<IEnumerable<GameInfo>> GetAllGamesAsync();
        Task<IEnumerable<ChatInfo>> GetAllChatsByGameNameAsync(string name);
        Task CreateGameAsync(GameInfo gameInfo);
        Task CreateChatAsync(ChatInfo chatInfo);
        Task<bool> RemoveChatAsync(Guid chatId);
    }
}
