using GameHub.Models;

namespace GameHub.Data
{
    public interface IArticleRepo
    {
        void CreateArticle(Article article);
        bool SaveChanges();
    }
}
