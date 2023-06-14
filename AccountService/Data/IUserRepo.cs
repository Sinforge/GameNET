using AccountService.Models;
using ArticleService.IntegrationEvents;
using Newtonsoft.Json.Bson;

namespace AccountService.Data
{
    public interface IUserRepo
    {
        Task CreateUser(User user);
        Task<User> FindByEmailAndId(string email, string id);

        Task<User> FindById(string id);

        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserDataById(string id);

        Task CreateNotifications(UserCreateArticleEvent createArticleEvent);

        Task SubsribeToUser(string userId1, string userId2);

    }
}
