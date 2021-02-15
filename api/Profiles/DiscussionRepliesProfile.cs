using AutoMapper;
using Forum.Dtos;
using Forum.Models;

namespace Forum.Profiles
{
    public class DiscussionRepliesProfile : Profile
    {
        public DiscussionRepliesProfile()
        {
            CreateMap<DiscussionReplyCreateDto, DiscussionReply>();
            CreateMap<DiscussionReply, DiscussionReplyReadDto>();
            CreateMap<DiscussionReply, DiscussionReplyCreateDto>();
            CreateMap<DiscussionReplyUpdateDto, DiscussionReply>();
        }
    }
}