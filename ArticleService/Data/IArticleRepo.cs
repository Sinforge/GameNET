using ArticleService.DTOs;
using ArticleService.Models;

namespace ArticleService.Data
{
    public interface IArticleRepo
    {
        Task CreateArticle(Article article);

        Task<IEnumerable<Article>> GetAllArticles();

        Task<Article?> GetArticleById(int id);

        
        Task<IEnumerable<Article>> GetArticlesByUser(string userId);


        Task<IEnumerable<Comment>> GetCommentsByArticle(int id);

 

        Task CreateComment(CommentCreateDto comment);
    }
}
