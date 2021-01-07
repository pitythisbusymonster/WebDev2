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

        public IQueryable<Forum> Posts
        { 
            get { return posts.AsQueryable<Forum>(); }
        }


        public void AddPost(Forum post)
        {
            post.ForumID = posts.Count;
            posts.Add(post);
        }

        /*public Forum GetPostByPageName(string title)
        {
            throw new NotImplementedException();
        }*/
         
    }
}
