using ArticleService.Models;

namespace ArticleService.Data
{
    public interface IArticleRepo
    {
        void CreateArticle(Article article);
        bool SaveChanges();
    }
}
