using EventBus.Events;

namespace ArticleService.IntegrationEvents
{
    public interface IArticleIntegrationEventService
    {
        Task PublishToEventBusAsync(IntegrationEvent @event);
    }
}
