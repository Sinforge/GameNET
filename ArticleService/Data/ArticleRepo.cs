using ArticleService.Models;

namespace ArticleService.Data
{
    public class ArticleRepo : IArticleRepo
    {
        private readonly ApplicationContext _context;

        public ArticleRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateArticle(Article article)
        {
            _context.Articles.Add(article);
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _context.Articles.ToList();
        }

        public Article? GetArticleById(int id)
        {
            return _context.Articles.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Article> GetArticlesByUser(string userId)
        {
            return from article in _context.Articles where article.Owner == userId select article;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
