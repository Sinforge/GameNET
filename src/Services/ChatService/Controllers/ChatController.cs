using ChatService.Data;
using ChatService.Models;
using ChatService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService) {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("/game-chats")]
        public async Task<IEnumerable<GameInfo>> GetAllGames()
        {
            return await _chatService.GetAllGamesAsync(); 
        }
        [HttpGet]
        [Route("/game-chats/{gameName}")]
        public async Task<IEnumerable<ChatInfo>> GetAllChatsByGameName([FromRoute] string gameName)
        {
            return await _chatService.GetAllChatsByGameNameAsync(gameName) ;
        }

        [HttpPost]
        [Route("/game/create")]
        //[Authorize] TODO : add auth
        public async Task CreateGame([FromBody] GameInfo gameInfo)
        {
            await _chatService.CreateGameAsync(gameInfo);
        }
        [HttpPost]
        [Route("/game-chats/create")]
        public async Task CreateGameChat([FromBody] ChatInfo chatInfo)
        {
            await _chatService.CreateChatAsync(chatInfo);
        }
    }
}
