using ArticleService.DTOs;
using ArticleService.Models;
using AutoMapper;
using Dapper;
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

        public async Task CreateArticle(Article article)
        {
            var insertQuery = "INSERT INTO public.article (title, text, owner) values (@title, @text, @owner)";
            var @params = new DynamicParameters();
            @params.Add("title", article.Title);
            @params.Add("text", article.Text);
            @params.Add("owner", article.Owner);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, @params);
            }
            
        }

        public async Task CreateComment(CommentCreateDto comment)
        {
            var insertQuery = "INSERT INTO public.comment (date, content, owner, article_id, parent_id) " +
                "values (@date, @content, @owner, @article_id, @parent_id)";
            Comment commentToAdd = _mapper.Map<Comment>(comment);
            var @params = new DynamicParameters();
            @params.Add("date", commentToAdd.Date);
            @params.Add("content", commentToAdd.Content);
            @params.Add("owner", commentToAdd.Owner);
            @params.Add("article_id", commentToAdd.ArticleId);
            @params.Add("parent_id", commentToAdd.ParentId);
            using(var connection = _context.CreateConnection()) {
                await connection.ExecuteAsync(insertQuery, @params);
            }
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            var selectQuery = "SELECT * from public.article";
            using(var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Article>(selectQuery);
            }
        }

        public async Task<Article?> GetArticleById(int id)
        {
            var selectQuery = "SELECT * from public.article where id = @id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Article>(selectQuery, new { id });
            }
        }

        public async Task<IEnumerable<Article>> GetArticlesByUser(string userId)
        {
            var selectQuery = "SELECT * from public.article where owner = @owner";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Article>(selectQuery, new { owner = userId });
            }

        }
        

        //Note this method returns main article comments (with 0 reply level), to get replies use GetReplies method
        public async Task<IEnumerable<Comment>> GetCommentsByArticle(int id)
        {
            var selectQuery = "SELECT * from public.comment where article_id = @article_id";
            using (var connection = _context.CreateConnection())
                return await connection.QueryAsync<Comment>(selectQuery, id);
        }
        public async Task<IEnumerable<Comment>> GetReplies(int commentId)
        {
            var selectQuery = "SELECT * from public.comment where parent_id = @parent_id";
            using (var connection = _context.CreateConnection())
                return await connection.QueryAsync<Comment>(selectQuery, commentId);

        }


        //public void CreateArticle(Article article)
        //{
        //    _context.Articles.Add(article);
        //}

        //public async Task CreateComment(CommentCreateDto comment)
        //{
        //    Comment commentToAdd = _mapper.Map<Comment>(comment);
        //    commentToAdd.Article = _context.Articles.FirstOrDefault(a => a.Id == comment.ArticleId);
        //    _context.Comments.Add(commentToAdd);
        //    if(comment.FatherCommentId != -1)
        //    {
        //        Comment fatherComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.FatherCommentId);
        //        fatherComment?.Replies.Add(commentToAdd);

        //    }
        //    _context.SaveChanges();

        //}

        //public IEnumerable<Article> GetAllArticles()
        //{
        //    return _context.Articles.ToList();
        //}

        //public Article? GetArticleById(int id)
        //{
        //    return _context.Articles.FirstOrDefault(a => a.Id == id);
        //}

        //public IEnumerable<Article> GetArticlesByUser(string userId)
        //{
        //    return from article in _context.Articles where article.Owner == userId select article;
        //}

        //public IEnumerable<Comment> GetCommentsByArticle(int id)
        //{
        //    return from comment in _context.Comments where comment.Article.Id == id select comment;
        //}

        //public bool SaveChanges()
        //{
        //    return _context.SaveChanges() >= 0;
        //}
    }
}
