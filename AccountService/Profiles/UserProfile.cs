using AccountService.Dtos;
using AccountService.Models;
using AutoMapper;

namespace AccountService.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            // Source --> Target
            CreateMap<UserCreateDto, User>();

        }
    }
}
