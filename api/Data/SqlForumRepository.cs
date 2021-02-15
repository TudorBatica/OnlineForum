using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Forum.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data 
{
    public class SqlForumRepository : IForumRepository
    {
        private readonly ForumContext _context;

        public SqlForumRepository(ForumContext context)
        {
            _context = context;
        }

        // Discussions
        public void CreateDiscussion(Discussion discussion)
        {
            if(discussion is null) 
            {
                throw new ArgumentNullException(nameof(discussion));
            };
            _context.Add(discussion);
        }

        public void DeleteDiscussion(Discussion discussion)
        {
            if(discussion is null)
            {
                throw new ArgumentNullException(nameof(discussion));
            }
            _context.Discussions.Remove(discussion);
        }

        public IEnumerable<Discussion> GetAllDiscussions(string query, string sortBy, string sortOrder, 
                                                        List<int> discussionTypes, List<int> careers,
                                                        int page, int pageSize, string embed)
        {
            IQueryable<Discussion> discussions = _context.Discussions
                                    .Include(d => d.DiscussionType)
                                    .Include(d => d.Career);
            
            // match title
            if(!string.IsNullOrEmpty(query))
            {
                discussions = discussions.Where(d => d.Title.ToLower().Contains(query.ToLower()));
            }

            // filter
            if( (discussionTypes != null) && (discussionTypes.Any()))
            {
                discussions = discussions.Where(d => discussionTypes.Contains(d.DiscussionTypeId));
            }
            
            if((careers != null) && (careers.Any()))
            {
                discussions = discussions.Where(d => careers.Contains(d.CareerId));
            }

            // sort -> must be cleaned up
            if(sortBy.Equals("views"))
            {
                if(sortOrder.Equals("asc"))
                {
                    discussions = discussions.OrderBy(d => d.NoOfViews);
                }
                else
                {
                    discussions = discussions.OrderByDescending(d => d.NoOfViews);
                }
            }
            else
            {
                if(sortOrder.Equals("asc"))
                {
                    discussions = discussions.OrderBy(d => d.DiscussionDateTime);
                }
                else
                {
                    discussions = discussions.OrderByDescending(d => d.DiscussionDateTime);
                }
            }

            // embed replies
            if(embed.ToLower().Equals("true"))
            {
                discussions = discussions.Include(d => d.DiscussionReplies);
            }

            // pagination
            var skip = (page - 1) * pageSize;
            return discussions.Skip(skip).Take(pageSize).ToList();
        }
 
        public Discussion GetDiscussionById(int id, string embed)
        {
            IQueryable<Discussion> discussion = _context.Discussions
                                    .Include(d => d.DiscussionType)
                                    .Include(d => d.Career);
            
            if(embed.ToLower().Equals("true"))
            {
                discussion = discussion.Include(d => d.DiscussionReplies);
            }
            
            return discussion.FirstOrDefault(d => d.DiscussionId.Equals(id));
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0) ;
        }

        public void UpdateDiscussion(Discussion discussion)
        {
            //Nothing
        }

        // Discussion Replies
        public IEnumerable<DiscussionReply> GetAllDiscussionReplies(int? did)
        {   
            if(did is null)
            {
                return _context.DiscussionReplies.OrderByDescending(r => r.DiscussionReplyDateTime).ToList();
            }

            return _context.DiscussionReplies.Where(r => r.DiscussionId.Equals(did))
                .OrderByDescending(r => r.DiscussionReplyDateTime)
                .ToList();
        }

        public DiscussionReply GetDiscussionReplyById(int id)
        {
            return _context.DiscussionReplies.Find(id);
        }

        public void UpdateDiscussionReply(DiscussionReply discussionReply)
        {
            //Nothing
        }
        public void CreateDiscussionReply(DiscussionReply discussionReply)
        {
            if(discussionReply is null)
            {
                throw new ArgumentNullException(nameof(discussionReply));
            }
            _context.Add(discussionReply);
        }

        public void DeleteDiscussionReply(DiscussionReply discussionReply)
        {
            if(discussionReply is null)
            {
                throw new ArgumentNullException(nameof(discussionReply));
            }
            _context.Remove(discussionReply);
        }
    }
}