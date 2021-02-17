using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class DiscussionType
    {
        public int DiscussionTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Discussion> Discussions { get; set; }
    }
}