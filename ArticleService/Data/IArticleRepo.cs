using ArticleService.Models;

namespace ArticleService.Data
{
    public interface IArticleRepo
    {
        void CreateArticle(Article article);

        IEnumerable<Article> GetAllArticles();

        Article? GetArticleById(int id);

        
        IEnumerable<Article> GetArticlesByUser(string userId);
        bool SaveChanges();
    }
}
