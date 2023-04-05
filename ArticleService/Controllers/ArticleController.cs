using System.Security.Claims;
using ArticleService.Data;
using ArticleService.DTOs;
using ArticleService.Models;
using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Controllers
{

    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleRepo _articleRepo;
        private readonly IMapper _mapper;

        public ArticleController(ILogger<ArticleController> logger, IArticleRepo articleRepo, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _articleRepo = articleRepo;
        }

        /// <summary>
        /// Return Hello World message.
        /// </summary>
        [HttpGet]
        [Route("/hello")]
        public ActionResult HelloWorld()
        {
            return Content("Hello world");
        }

        /// <summary>
        /// Create a article
        /// </summary>
        /// <remark>
        /// Sample of request:
        /// {
        ///     "Title": "Aboba",
        ///     "Text": "AVFAfa",
        ///     "Owner": "me"
        /// }
        /// </remark>
        /// <returns>Ok</returns>
        [HttpPost]
        [Route("/CreateArticle")]
        public IActionResult CreateArticle(ArticleCreateDto articleCreateDto)
        {
            _logger.LogInformation($"Request for creating new article");
            var article = _mapper.Map<Article>(articleCreateDto);
            _articleRepo.CreateArticle(article);
            _articleRepo.SaveChanges();
            _logger.LogInformation("Created new article");
            return Ok("Data saved");

        }


        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns>List of articles</returns>
        [HttpGet]
        [Route("/GetAllArticles")]
        public IActionResult GetAllArticles()
        {
            _logger.LogInformation($"Request for getting all articles");
            return Ok(_articleRepo.GetAllArticles());
        }



        /// <summary>
        /// Get article with id
        /// </summary>
        /// <param name="id">Id of article</param>
        /// <returns>Ok 200 and article, or 404 with error message</returns>
        [HttpGet]
        [Route("/GetArticle/{id}")]
        public IActionResult GetArticle(int id)
        {
            _logger.LogInformation($"Request for getting article by id-number : {id}");

            Article? article = _articleRepo.GetArticleById(id);
            if (article == null)
            {
                return NotFound("object with such id not found");
            }
            return Ok(article);
        }


        /// <summary>
        /// Get articles by user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>Ok 200 and list of article, or 404 with error message</returns>
        [HttpGet]
        [Route("/GetAllArticles/{userId}")]
        public IActionResult GetArticlesByUser(string userId)
        {
            _logger.LogInformation($"Request for getting articles by user : {userId}");
            return Ok(_articleRepo.GetArticlesByUser(userId));
        }


        [HttpPost]
        [Route("/PostComment")]
        public IActionResult PostComment(CommentCreateDto commentCreateDto)
        {
            _logger.LogInformation($"Post comment");
            _articleRepo.CreateComment(commentCreateDto);
            _articleRepo.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("/GetComments/{articleId}")]
        public IActionResult GetComments(int articleId) {
            _logger.LogInformation($"Request for all comments for article with id : {articleId}");
            return Ok(_articleRepo.GetCommentsByArticle(articleId));
        }

    }
}