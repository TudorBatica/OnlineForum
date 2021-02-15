using AutoMapper;
using Forum.Dtos;
using Forum.Models;

namespace Forum.Profiles
{
    public class DiscussionsProfile : Profile
    {
        public DiscussionsProfile()
        {
            CreateMap<Discussion, DiscussionReadDto>();
            CreateMap<DiscussionCreateDto, Discussion>(); 
            CreateMap<Discussion, DiscussionCreateDto>();
        }
    }
}