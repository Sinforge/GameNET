using ArticleService.DTOs;
using ArticleService.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Data
{
    public class ArticleRepo : IArticleRepo
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ArticleRepo(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void CreateArticle(Article article)
        {
            _context.Articles.Add(article);
        }

        public async Task CreateComment(CommentCreateDto comment)
        {
            Comment commentToAdd = _mapper.Map<Comment>(comment);
            commentToAdd.Article = _context.Articles.FirstOrDefault(a => a.Id == comment.ArticleId);
            _context.Comments.Add(commentToAdd);
            if(comment.FatherCommentId != -1)
            {
                Comment fatherComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.FatherCommentId);
                fatherComment?.Replies.Add(commentToAdd);
                
            }
            _context.SaveChanges();

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

        public IEnumerable<Comment> GetCommentsByArticle(int id)
        {
            return from comment in _context.Comments where comment.Article.Id == id select comment;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
