using EventBus.Abstractions;
using EventBus.Events;

namespace ArticleService.IntegrationEvents
{
    public class ArticleIntegrationEventService : IArticleIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ArticleIntegrationEventService> _logger;

        public ArticleIntegrationEventService(IEventBus eventBus, ILogger<ArticleIntegrationEventService> logger) {
            _eventBus = eventBus;
            _logger = logger;
        }
        public async Task PublishToEventBusAsync(IntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation($"Publishing integration event : {@event.Id} - {@event}");
                _eventBus.Publish(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Publishing integration event: {@event.Id} - {@event}");
            }
        }
    }
}
