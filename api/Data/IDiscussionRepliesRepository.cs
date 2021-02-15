using Forum.Models;
using System.Collections.Generic;

namespace Forum.Data
{
    public interface IDiscussionRepliesRepository : IRepository<DiscussionReply>
    {
        IEnumerable<DiscussionReply> GetDiscussionRepliesByDiscussionId(int id);
    }
}