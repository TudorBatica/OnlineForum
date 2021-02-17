using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        [MaxLength(350)]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public int DiscussionTypeId { get; set; }
        
        public DiscussionType DiscussionType { get; set; }
        
        public int CareerId { get; set; }
        public Career Career {get; set;}

        public int NoOfViews { get; set; } = 0;
        
        public DateTime DiscussionDateTime { get; set; }
        
        public ICollection<DiscussionReply> DiscussionReplies { get; set; } 
    }
}