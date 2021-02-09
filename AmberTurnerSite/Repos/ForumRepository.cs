using AmberTurnerSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberTurnerSite.Repos
{
    public class ForumRepository : IPosts    
    {
        private ForumContext context;

        //constructor
        public ForumRepository(ForumContext c)
        {
            context = c;
        }

        public IQueryable<Forum> Posts 
        { 
            get 
            { 
                return context.Posts.Include(post => post.PostCreator)
                                    .Include(post => post.Replies)
                                    .ThenInclude(reply => reply.Replier); 
            } 
        }
        

        public void AddPost(Forum post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void UpdatePost(Forum post)
        {
            context.Posts.Update(post);
            context.SaveChanges();
        }
    }
}
