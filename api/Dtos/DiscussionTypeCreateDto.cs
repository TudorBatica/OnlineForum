using System.ComponentModel.DataAnnotations;

namespace Forum.Dtos
{
    public class DiscussionTypeCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}