using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class DiscussionReply
    {
        public int DiscussionReplyId { get; set; }

        [Required]
        public string Username { get; set; }
        public int DiscussionId { get; set; }
        public string Description { get; set; }
        public DateTime DiscussionReplyDateTime { get; set; }
    } 
}