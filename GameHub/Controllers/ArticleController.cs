using System.Security.Claims;
using AutoMapper;
using GameHub.Data;
using GameHub.DTOs;
using GameHub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHub.Controllers
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



        [HttpGet("/registration")]
        public ActionResult CreateUser(ArticleCreateDto articleCreateDto)
        {


            User? user = db.Users.FirstOrDefault(u => u.UserId == userId || u.Email == email);
            if (user != null)
            {
                _logger.LogInformation("User with such Id or Email exist");
            }

            db.Add(new User
            {
                UserId = userId,
                Name = userName,
                Password = password,
                Email = email,
                Role = Role.DefaultUser
            });

            await db.SaveChangesAsync();
        }


        [HttpPost("/article")]
        public ActionResult CreateArticle(ArticleCreateDto articleCreateDto)
        {
            var article = _mapper.Map<Article>(articleCreateDto);
            _articleRepo.CreateArticle(article);
            _articleRepo.SaveChanges();
            _logger.LogInformation("Created new article");
            return Ok();

        }

    }