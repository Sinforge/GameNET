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



        [HttpGet]
        [Route("/hello")]
        public ActionResult HelloWorld()
        {
            return Content("Hello world");
        }
        [HttpPost]
        [Route("/CreateArticle")]
        public ActionResult CreateArticle(ArticleCreateDto articleCreateDto)
        {
            var article = _mapper.Map<Article>(articleCreateDto);
            _articleRepo.CreateArticle(article);
            _articleRepo.SaveChanges();
            _logger.LogInformation("Created new article");
            return Ok("Data saved");

        }

    }
}