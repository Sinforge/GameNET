using EventBus.Events;
using System.Runtime.Serialization;

namespace AccountService.IntegrationEvents.Events
{
    public record UserCreateArticleIntegrationEvent : IntegrationEvent
    {

        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; } = null!;
        public string UserId { get; set; }

        public UserCreateArticleIntegrationEvent(int ArtcileId, string ArticleTitle, string UserId)
        {
            ArticleId = ArtcileId;
            this.ArticleTitle = ArticleTitle;
            this.UserId = UserId;
        }
    }
}
