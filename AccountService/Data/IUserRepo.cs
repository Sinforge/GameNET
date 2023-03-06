using AccountService.Models;

namespace AccountService.Data
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        User FindByEmailAndId(string email, string id);

        User FindById(string id);

        IEnumerable<User> GetAllUsers();

        bool SaveChanges();

    }
}
