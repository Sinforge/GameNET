using AutoMapper;
using ArticleService.DTOs;
using ArticleService.Models;

namespace ArticleService.Profiles
{
    public class ArticleProfile: Profile
    {
        public ArticleProfile()
        {
            // Source --> Target
            CreateMap<ArticleCreateDto, Article>();
            CreateMap<CommentCreateDto, Comment>();
        }
    }
}
