using AccountService.Models;
using ArticleService.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace AccountService.Data
{
    public class UserRepo: IUserRepo
    {
        private readonly ApplicationContext _context;

        public UserRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateNotifications(UserCreateArticleEvent createArticleEvent)
        {
            var userSubsribers  = _context.Users.Include(u => u.Subscribers).FirstOrDefault(u => u.UserId == createArticleEvent.UserId)?.Subscribers;
            if (userSubsribers != null)
            {
                foreach (User user in userSubsribers)
                {
                    Notification notification = new Notification();
                    notification.Text = createArticleEvent.ArticleTitle;
                    notification.User = user;
                    notification.isChecked = false;
                    notification.Url = "pass";
                    _context.Notifications.Add(notification);
                    user.Notifications.Add(notification);

                }
                _context.SaveChanges();
            }
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

        public void SubsribeToUser(string userId1, string userId2)
        {
            var user1 = _context.Users.Include(u => u.Subscriptions).FirstOrDefault(x => x.UserId == userId1);
            var user2 = _context.Users.Include(u => u.Subscribers).FirstOrDefault(x =>x.UserId == userId2);
            user1.Subscriptions.Add(user2);
            user2.Subscribers.Add(user1);
            _context.SaveChanges();

        }
    }
}
