using System.Runtime.Serialization;

namespace ArticleService.IntegrationEvents
{
    public class UserCreateArticleEvent
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; } = null!;
        public string UserId { get; set; }

        public UserCreateArticleEvent(int articleId, string articleTitle, string userId)
        {
            ArticleId = articleId;
            ArticleTitle = articleTitle;
            UserId = userId;
        }
    }
}
