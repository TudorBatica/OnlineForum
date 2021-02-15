using System.Collections.Generic;
using System.Linq;
using Forum.Models;

namespace Forum.Data
{
    public class DiscussionRepliesRepository : Repository<DiscussionReply>, IDiscussionRepliesRepository
    {
        public DiscussionRepliesRepository(ForumContext context) : base(context)
        {
        }

        public IEnumerable<DiscussionReply> GetDiscussionRepliesByDiscussionId(int discussionId)
        {
            return Context.Set<DiscussionReply>().Where(r => r.DiscussionId.Equals(discussionId))
                .OrderByDescending(r => r.DiscussionReplyDateTime)
                .ToList();
        }
    }
}