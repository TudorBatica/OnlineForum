using System.Collections.Generic;
using System.Security.Claims;
using Forum.Models;

namespace Forum.Data
{
    public interface IForumRepository
    {
        bool SaveChanges();

        // Discussions
        IEnumerable<Discussion> GetAllDiscussions(string query, string sortBy, string sortOrder,
                                                        List<int> discussionTypes, List<int> careers,
                                                        int page, int pageSize, string embed);

        Discussion GetDiscussionById(int id, string embed);
        void CreateDiscussion(Discussion discussion);
        void UpdateDiscussion(Discussion discussion);
        void DeleteDiscussion(Discussion discussion);

        // Discussion Replies
        IEnumerable<DiscussionReply> GetAllDiscussionReplies(int? did);
        DiscussionReply GetDiscussionReplyById(int id);
        void CreateDiscussionReply(DiscussionReply discussionReply);
        void UpdateDiscussionReply(DiscussionReply discussionReply);
        void DeleteDiscussionReply(DiscussionReply discussionReply);
    }
}