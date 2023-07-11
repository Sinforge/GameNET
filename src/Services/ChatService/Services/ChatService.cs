using ChatService.Data;
using ChatService.Models;

namespace ChatService.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepo _chatRepo;
        public ChatService(IChatRepo chatRepo)
        {
            _chatRepo = chatRepo;
        }

        public async Task CreateChatAsync(ChatInfo chatInfo)
        {
            await _chatRepo.CreateChatAsync(chatInfo);
        }

        public async Task CreateGameAsync(GameInfo gameInfo)
        {
            await _chatRepo.CreateGameAsync(gameInfo);
                    
        }

        public async Task<IEnumerable<ChatInfo>> GetAllChatsByGameNameAsync(string name)
        {
            return await _chatRepo.GetAllChatsByGameNameAsync(name);
        }

        public async Task<IEnumerable<GameInfo>> GetAllGamesAsync()
        {
            return await _chatRepo.GetAllGamesAsync();
        }

        public async Task<bool> RemoveChatAsync(Guid chatId)
        {
            return await _chatRepo.RemoveChatAsync(chatId);
        }
    }
}
