using System;

namespace Forum.Dtos
{
    public class DiscussionReplyReadDto
    {
        public int DiscussionReplyId { get; set; }
        public int DiscussionId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public DateTime DiscussionReplyDateTime { get; set; }
    }
}