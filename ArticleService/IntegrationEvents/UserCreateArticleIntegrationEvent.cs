using System.Runtime.Serialization;

namespace ArticleService.IntegrationEvents
{
    public class UserCreateArticleIntegrationEvent
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; } = null!;
        public string UserId { get; set; }

        public UserCreateArticleIntegrationEvent(int articleId, string articleTitle, string userId)
        {
            ArticleId = articleId;
            ArticleTitle = articleTitle;
            UserId = userId;
        }
    }
}
