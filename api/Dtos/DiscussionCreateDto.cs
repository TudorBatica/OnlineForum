using System.ComponentModel.DataAnnotations;

namespace Forum.Dtos
{
    public class DiscussionCreateDto
    {
        [Required]
        [MaxLength(350)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int DiscussionTypeId { get; set; }

        [Required]
        public int CareerId { get; set; }
    }
}