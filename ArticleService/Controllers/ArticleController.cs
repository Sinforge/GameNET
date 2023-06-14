using System.Security.Claims;
using ArticleService.Data;
using ArticleService.DTOs;
using ArticleService.IntegrationEvents;
using ArticleService.Models;
using ArticleService.Services;
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
        //private readonly IMessageProducer _messageProducer;
        private readonly IMapper _mapper;

        public ArticleController(ILogger<ArticleController> logger, IArticleRepo articleRepo, IMapper mapper
            )//IMessageProducer messageProducer)
        {
            //_messageProducer = messageProducer;
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
        public async Task<IActionResult> CreateArticle(ArticleCreateDto articleCreateDto)
        {
            _logger.LogInformation($"Request for creating new article");
            var article = _mapper.Map<Article>(articleCreateDto);
            await _articleRepo.CreateArticle(article);
            _logger.LogInformation("Created new article");
            //_messageProducer.SendMessage<UserCreateArticleEvent>(
            //    new UserCreateArticleEvent(
            //        article.Id,
            //        article.Title,
            //        article.Owner
            //        )
            // );

            return Ok("Data saved");

        }


        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns>List of articles</returns>
        [HttpGet]
        [Route("/GetAllArticles")]
        public async Task<IActionResult> GetAllArticles()
        {
            _logger.LogInformation($"Request for getting all articles");
            return Ok(await _articleRepo.GetAllArticles());
        }



        /// <summary>
        /// Get article with id
        /// </summary>
        /// <param name="id">Id of article</param>
        /// <returns>Ok 200 and article, or 404 with error message</returns>
        [HttpGet]
        [Route("/GetArticle/{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            _logger.LogInformation($"Request for getting article by id-number : {id}");

            Article? article = await _articleRepo.GetArticleById(id);
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
        public async Task<IActionResult> GetArticlesByUser(string userId)
        {
            _logger.LogInformation($"Request for getting articles by user : {userId}");
            return Ok(await _articleRepo.GetArticlesByUser(userId));
        }


        [HttpPost]
        [Route("/PostComment")]
        public async Task<IActionResult> PostComment(CommentCreateDto commentCreateDto)
        {
            _logger.LogInformation($"Post comment");
            await _articleRepo.CreateComment(commentCreateDto);
            return Ok();
        }

        [HttpGet]
        [Route("/GetComments/{articleId}")]
        public async Task<IActionResult> GetComments(int articleId) {
            _logger.LogInformation($"Request for all comments for article with id : {articleId}");
            return Ok(await _articleRepo.GetCommentsByArticle(articleId));
        }

    }
}