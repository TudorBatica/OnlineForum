using Forum.Models;

namespace Forum.Data
{
    public class CareersRepository : Repository<Career>, ICareersRepository
    {
        public CareersRepository(ForumContext context) : base(context)
        {
        }
    }
}