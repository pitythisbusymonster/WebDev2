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
                return context.Posts.Include(post => post.PostCreator); 
            } 
        }
        

        public void AddPost(Forum post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

    }
}
