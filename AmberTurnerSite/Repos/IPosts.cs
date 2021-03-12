using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmberTurnerSite.Models;

namespace AmberTurnerSite.Repos
{
    public interface IPosts
    {
        IQueryable<Forum> Posts { get; }    // Read (or retrieve) posts
        void AddPost(Forum posts);          // Create a post

        Forum GetPost(int id);   //(string postCreator); 

        void UpdatePost(Forum posts);


        IQueryable<Reply> Replies { get; }
        Reply GetReply(int id);
        void AddReply(Reply replies);
    }

}
