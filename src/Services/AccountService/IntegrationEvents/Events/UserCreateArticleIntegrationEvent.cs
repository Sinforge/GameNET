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
        public string ArticleTitle { get; set; }
        [JsonInclude]
        public string UserId { get; set; }
        [JsonConstructor]
        public UserCreateArticleIntegrationEvent(int ArticleId, string ArticleTitle, string UserId)
        {
            this.ArticleId = ArticleId;
            this.ArticleTitle = ArticleTitle;
            this.UserId = UserId;
        }
       
    }
}
