using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Data
{
    public class UserRepo: IUserRepo
    {
        private readonly ApplicationContext _context;

        public UserRepo(ApplicationContext context)
        {
            _context = context;
        }
        public void CreateUser(User user)
        {

            _context.Users.Add(user);
        }

        public User FindByEmailAndId(string email, string id)
        { 
            return _context.Users.FirstOrDefault(u => u.UserId == id && u.Email == email);
        }

        public User FindById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<User?> GetUserDataById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}
