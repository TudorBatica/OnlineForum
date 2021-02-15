using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Career
    {
        public int CareerId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public ICollection<Discussion> Discussions { get; set; }
    }
}