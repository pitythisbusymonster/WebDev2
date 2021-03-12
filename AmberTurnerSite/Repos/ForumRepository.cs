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

        public Forum GetPost(int id)
        {
            return context.Posts.Include("id").First(b => b.ForumID == id);
            //throw new NotImplementedException();
        }


        public void UpdatePost(Forum post)
        {
            context.Posts.Update(post);
            context.SaveChanges();
        }







        public IQueryable<Reply> Replies
        {
            get
            {
                return context.Replies;//.Include(reply => reply.Replier)
                                    //.Include(reply => reply.Replies)
                                    //.ThenInclude(reply => reply.Replier);
            }
        }

        public Reply GetReply(int id)
        {
            return context.Replies.Include("id").First(b => b.ReplyID == id);
            //throw new NotImplementedException();
        }

        public void AddReply(Reply reply)
        {
            context.Replies.Add(reply);
            context.SaveChanges();
        }
    }
}
