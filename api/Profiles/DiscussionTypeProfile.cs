using AutoMapper;
using Forum.Dtos;
using Forum.Models;

namespace Forum.Profiles
{
    public class DiscussionTypeProfile : Profile
    {
        public DiscussionTypeProfile()
        {
            CreateMap<DiscussionTypeCreateDto, DiscussionType>();
            CreateMap<DiscussionType, DiscussionTypeReadDto>();
            CreateMap<DiscussionType, DiscussionTypeCreateDto>();
        }
    }
}