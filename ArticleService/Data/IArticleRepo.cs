using ArticleService.Models;

namespace ArticleService.Data
{
    public interface IArticleRepo
    {
        void CreateArticle(Article article);

        IEnumerable<Article> GetAllArticles();

        Article? GetArticleById(int id);
        bool SaveChanges();
    }
}
