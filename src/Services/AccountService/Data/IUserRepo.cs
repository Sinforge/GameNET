using AccountService.Models;
using AccountService.IntegrationEvents.Events;
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

        Task CreateNotifications(UserCreateArticleIntegrationEvent createArticleEvent);

        Task SubsribeToUser(string userId1, string userId2);

    }
}
