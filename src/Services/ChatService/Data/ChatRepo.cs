using ChatService.Models;
using Dapper;

namespace ChatService.Data
{
    public class ChatRepo : IChatRepo
    {
        private readonly ApplicationContext _context;
        
        public ChatRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateChatAsync(ChatInfo chatInfo)
        {
            string insertQuery = "insert into chat (id, title, game_id) values (@id, @title, @game_id)";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.AddDynamicParams(new
            {
                id = Guid.NewGuid(),
                title = chatInfo.Title,
                game_id = chatInfo.GameId,
            });
            using(var connection =  _context.CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, dynamicParameters);
            }
        }

        public async Task CreateGameAsync(GameInfo gameInfo)
        {
            string insertQuery = "insert into game(id, name) values (@id, @name);";
            string checkAndInsertQuery = "insert into tag(name) select @name where not exists(select name from tag where name = @name)";
            string insertQuery2 = "insert into game_tag(game_id, tag_id) values (@id , (select id from tag where name = @name limit 1))";
            gameInfo.Id = Guid.NewGuid();
            using(var connection = _context.CreateConnection())
            {
                connection.Open();
                using(var transaction =  connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(insertQuery, new { id = gameInfo.Id, name = gameInfo.Name });

                    foreach (string tag in gameInfo.Tags)
                    {
                        await connection.ExecuteAsync(checkAndInsertQuery, new { name = tag });
                        await connection.ExecuteAsync(insertQuery2, new { id = gameInfo.Id, name = tag });
                    }
                    transaction.Commit();
                }
                
            }
        }

        public async Task<IEnumerable<ChatInfo>> GetAllChatsByGameNameAsync(string name)
        {
            string selectQuery = "select  chat.id, chat.title " +
                "from chat inner join game " +
                "on chat.game_id = game.id " +
                "where game.name = @name";
            IEnumerable<ChatInfo> gameChats;
            using(var connection = _context.CreateConnection())
            {
                gameChats = await connection.QueryAsync<ChatInfo>(selectQuery, new {name = name});
            }
            return gameChats;
        }

        public async Task<IEnumerable<GameInfo>> GetAllGamesAsync()
        {
            string selectQuery = "SELECT * from game";
            string selectTagsQuery = "SELECT name from tag where id in (SELECT tag_id from game_tag where game_id = @game_id)";
            IEnumerable<GameInfo> games;
            using(var connection =  _context.CreateConnection())
            {
                games = await connection.QueryAsync<GameInfo>(selectQuery);
                foreach(GameInfo gameInfo in games)
                {
                    gameInfo.Tags = (ICollection<string>)await connection.QueryAsync<string>(selectTagsQuery, new {game_id = gameInfo.Id});
                }
                
            }
            return games;

        }

        public async Task<bool> RemoveChatAsync(Guid chatId)
        {
            string deleteQuery = "delete from chat where id = @id";
            try
            {

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(deleteQuery, new { id = chatId });
                }
            } catch(Exception ex)
            {
                //log this event !!!
                return false;
            }
            return true;
        }

        
    }
}
