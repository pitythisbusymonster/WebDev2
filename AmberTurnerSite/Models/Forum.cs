using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberTurnerSite.Models
{
    public class Forum
    {
        //these become foreign keys, for db
        public int ForumID {get; set;} //
        public string PageName { get; set; }
        public string PageRating { get; set; }
        public string PostText { get; set; }
        public User PostCreator { get; set; }
        public DateTime PostDate { get; set; }

        
    }
}
