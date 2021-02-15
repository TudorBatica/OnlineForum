using System;
using System.Collections.Generic;
using Forum.Models;

namespace Forum.Dtos
{
    public class DiscussionReadDto
    {
        public int DiscussionId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NoOfViews { get; set; }
        public DateTime DiscussionDateTime { get; set; }
        public DiscussionTypeReadDto DiscussionType { get; set; }   
        public CareerReadDto Career { get; set; }
        public ICollection<DiscussionReply> DiscussionReplies { get; set; }
    }
}