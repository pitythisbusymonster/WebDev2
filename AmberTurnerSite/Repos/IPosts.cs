using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmberTurnerSite.Models;

namespace AmberTurnerSite.Repos
{
    public interface IPosts  //should be IPosts by convention
    {    
        IQueryable<Forum> Posts { get; }    //read/retrieve reviews

        void AddPost(Forum forum);  //create a review

        Forum GetPostByPageName(string title);    //retrieve a particualr review

    }
}
