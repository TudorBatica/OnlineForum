using Forum.Models;
using System.Collections.Generic;
using Predicate = System.Linq.Expressions.Expression<System.Func<Forum.Models.Discussion, bool>>;

namespace Forum.Data
{
    public interface IDiscussionRepository : IRepository<Discussion>
    {
        IEnumerable<Discussion> GetDiscussionsUsingParameters(IEnumerable<Predicate> filters,
            string sortParam,
            bool orderAscending);
    }
}