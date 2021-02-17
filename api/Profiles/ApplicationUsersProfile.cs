using AutoMapper;
using Forum.Dtos;
using Forum.Models;

namespace Forum.Profiles
{
    public class ApplicationUsersProfile : Profile
    {
        public ApplicationUsersProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserReadDto>();
        }
    }
}