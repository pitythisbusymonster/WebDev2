using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmberTurnerSite.Models;

namespace AmberTurnerSite.Repos
{
    public class FakeForumRepository : IPosts
    {        
        List<Forum> posts = new List<Forum>();
        //List<Reply> replies = new List<Reply>();

        public IQueryable<Forum> Posts
        { 
            get { return posts.AsQueryable<Forum>(); }
        }


        public void AddPost(Forum post)
        {
            post.ForumID = posts.Count;
            posts.Add(post);
        }

        public Forum GetPost(int id)  //(string postCreator)
        {
            throw new NotImplementedException();
        }


        public void UpdatePost(Forum post)
        {
            //context.Posts.Update(post);
            //context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
