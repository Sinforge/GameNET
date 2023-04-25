using AccountService.Models;
using ArticleService.IntegrationEvents;
using Newtonsoft.Json.Bson;

namespace AccountService.Data
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        User FindByEmailAndId(string email, string id);

        User FindById(string id);

        IEnumerable<User> GetAllUsers();

        Task<User?> GetUserDataById(string id);
        bool SaveChanges();

        void CreateNotifications(UserCreateArticleIntegrationEvent createArticleEvent);

        void SubsribeToUser(string userId1, string userId2);

    }
}
