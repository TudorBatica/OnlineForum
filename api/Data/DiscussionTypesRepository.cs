using Forum.Models;

namespace Forum.Data
{
    public class DiscussionTypesRepository : Repository<DiscussionType>, IDiscussionTypesRepository
    {
        public DiscussionTypesRepository(ForumContext context) : base(context)
        {
        }
    }
}