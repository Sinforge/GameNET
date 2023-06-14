using AccountService.Models;
using ArticleService.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System;
using Dapper;

namespace AccountService.Data
{
    public class UserRepo: IUserRepo
    {
        private readonly ApplicationContext _context;

        public UserRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateNotifications(UserCreateArticleEvent createArticleEvent)
        {
            var selectQuery = "SELECT subscriber_id FROM 'subscription where user_id = @publisher";
            var insertQuery = "INSERT INTO notification (HtmlText, UserId) VALUES (@content, @receiver)";
            IEnumerable<int> subscribers;

            using (var connection = _context.CreateConnection())
            {
                subscribers = await connection.QueryAsync<int>(selectQuery, createArticleEvent.UserId);
                if (subscribers.Any())
                {
                    string messageContent = $"User {createArticleEvent.UserId} create new article check this. Article id: {createArticleEvent.ArticleId}";
                    foreach (var subscriber in subscribers)
                    {
                        var @params = new DynamicParameters();
                        @params.Add("content", messageContent);
                        @params.Add("receiver", subscriber);
                        await connection.ExecuteAsync(insertQuery, @params);
                    }
                }

            }

        }

        public async Task CreateUser(User user)
        {
            var insertQuery = "INSERT INTO user (Id, Name, Password, Email, Role) VALUES (@Id, @Name, @Password, @Email, @Role)";
            var @params = new DynamicParameters();
            @params.Add("Id", user.UserId);
            @params.Add("Name", user.Name);
            @params.Add("Password", user.Password);
            @params.Add("Email", user.Email);
            @params.Add("UserId", Role.DefaultUser, System.Data.DbType.Int16);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, @params);
            }
        }

        public async Task<User> FindByEmailAndId(string email, string id)
        {
            var selectQuery = "SELECT * FROM user WHERE \"Id\" = @Id OR \"Email\" = @Email";
            var @params = new DynamicParameters();
            @params.Add("Id", id);
            @params.Add("Email", email);
            using(var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(selectQuery, @params);
            }
        }

        public async Task<User> FindById(string id)
        {
            var selectQuery = "SELECT * FROM user WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(selectQuery, new { id });
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var selectQuery = "SELECT * FROM user";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<User>(selectQuery);
            }
        }

        public async Task<User?> GetUserDataById(string id)
        {
            return await FindById(id);
        }



        public async Task SubsribeToUser(string userId1, string userId2)
        {
            var insertQuery = "INSERT INTO subscription (user_id, subscriber_id) values (@u1, @u2)";
            var @params = new DynamicParameters();
            @params.Add("u1", userId1);
            @params.Add("u2", userId2);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, @params);

            }
        }

        //public void CreateNotifications(UserCreateArticleEvent createArticleEvent)
        //{
    //    var userSubsribers = _context.Users.Include(u => u.Subscribers).FirstOrDefault(u => u.UserId == createArticleEvent.UserId)?.Subscribers;
    //        if (userSubsribers != null)
    //        {
    //            foreach (User user in userSubsribers)
    //            {
    //                Notification notification = new Notification();
    //    notification.Text = createArticleEvent.ArticleTitle;
    //                notification.User = user;
    //                notification.isChecked = false;
    //                notification.Url = "pass";
    //                _context.Notifications.Add(notification);
    //                user.Notifications.Add(notification);

    //            }
    //_context.SaveChanges();
    //        }
        //}


        //public void CreateUser(User user)
        //{

        //    _context.Users.Add(user);
        //}

        //public User FindByEmailAndId(string email, string id)
        //{ 
        //    return _context.Users.FirstOrDefault(u => u.UserId == id && u.Email == email);
        //}

        //public User FindById(string id)
        //{
        //    return _context.Users.FirstOrDefault(u => u.UserId == id);
        //}

        //public IEnumerable<User> GetAllUsers()
        //{
        //    return _context.Users.ToList();
        //}

        //public async Task<User?> GetUserDataById(string id)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        //}

        //public bool SaveChanges()
        //{
        //    return _context.SaveChanges() >= 0;
        //}

        //public void SubsribeToUser(string userId1, string userId2)
        //{
        //    var user1 = _context.Users.Include(u => u.Subscriptions).FirstOrDefault(x => x.UserId == userId1);
        //    var user2 = _context.Users.Include(u => u.Subscribers).FirstOrDefault(x =>x.UserId == userId2);
        //    user1.Subscriptions.Add(user2);
        //    user2.Subscribers.Add(user1);
        //    _context.SaveChanges();

        //}


    }
}
