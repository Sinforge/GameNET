using EventBus.Events;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AccountService.IntegrationEvents.Events
{
    public record UserCreateArticleIntegrationEvent : IntegrationEvent
    {
        [JsonInclude]
        public int ArticleId { get; set; }
        [JsonInclude]
        public string ArticleTitle { get; set; } = null!;
        [JsonInclude]
        public string UserId { get; set; }

        public UserCreateArticleIntegrationEvent(int ArtcileId, string ArticleTitle, string UserId)
        {
            this.ArticleId = ArtcileId;
            this.ArticleTitle = ArticleTitle;
            this.UserId = UserId;
        }
       
    }
}
