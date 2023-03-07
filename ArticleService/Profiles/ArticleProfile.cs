using AutoMapper;
using GameHub.DTOs;
using GameHub.Models;

namespace GameHub.Profiles
{
    public class ArticleProfile: Profile
    {
        public ArticleProfile()
        {
            // Source --> Target
            CreateMap<ArticleCreateDto, Article>();

        }
    }
}
