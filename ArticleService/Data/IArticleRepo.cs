using ArticleService.DTOs;
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


        IEnumerable<Comment> GetCommentsByArticle(int id);

 

        Task CreateComment(CommentCreateDto comment);
    }
}
