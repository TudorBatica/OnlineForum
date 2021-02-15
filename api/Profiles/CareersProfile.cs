using AutoMapper;
using Forum.Dtos;
using Forum.Models;

namespace Forum.Profiles
{
    public class CareersProfile : Profile
    {
        public CareersProfile()
        {
            CreateMap<Career, CareerReadDto>();
        }
    }
}