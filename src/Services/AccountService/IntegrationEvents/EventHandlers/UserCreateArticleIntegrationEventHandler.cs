using EventBus.Abstractions;
using AccountService.IntegrationEvents.Events;
using AccountService.Data;
using System.Text;

namespace AccountService.IntegrationEvents.EventHandlers
{
    public class UserCreateArticleIntegrationEventHandler : IIntegrationEventHandler<UserCreateArticleIntegrationEvent>
    {
        private readonly IUserRepo _userRepo;
        public UserCreateArticleIntegrationEventHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
       

        public async Task Handle(UserCreateArticleIntegrationEvent @event)
        {
            Console.WriteLine($"We received data {@event.Id} {@event.UserId} {@event.ArticleTitle}");
            await _userRepo.CreateNotifications(@event);
        }
    }
}
