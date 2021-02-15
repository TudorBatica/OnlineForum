using System;
using System.Collections.Generic;
using System.Linq;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Predicate = System.Linq.Expressions.Expression<System.Func<Forum.Models.Discussion, bool>>;

namespace Forum.Data
{
    public class DiscussionsRepository : Repository<Discussion>, IDiscussionRepository
    {
        public DiscussionsRepository(ForumContext context) : base(context)
        {
        }

        public PagedList<Discussion> GetDiscussionsUsingParameters(
            IEnumerable<Predicate> filters,
            string sortParam,
            bool orderAscending,
            int page,
            int pageSize)
        {
            IQueryable<Discussion> discussions = Context.Set<Discussion>()
                                                .Include(d => d.Career).Include(d => d.DiscussionType);
            discussions = Filter(discussions, filters);
            discussions = Sort(discussions, sortParam, orderAscending);
            
            return PagedList<Discussion>.ToPagedList(discussions, page, pageSize);
        }
        private IQueryable<Discussion> Filter(IQueryable<Discussion> discussions, IEnumerable<Predicate> filters)
        {
            foreach(var filter in filters)
            {
                discussions = discussions.Where(filter);
            }
            return discussions;
        }
        private IQueryable<Discussion> Sort(IQueryable<Discussion> discussions, string param, bool orderAscending)
        {
            if(param.Equals("views"))
            {
                if(orderAscending) 
                    return discussions.OrderBy(d => d.NoOfViews);
                
                return discussions.OrderByDescending(d => d.NoOfViews);
            }
            else 
            {
                if(orderAscending) 
                    return discussions.OrderBy(d => d.DiscussionDateTime);
                
                return discussions.OrderByDescending(d => d.DiscussionDateTime);
            }

        }
    }
}