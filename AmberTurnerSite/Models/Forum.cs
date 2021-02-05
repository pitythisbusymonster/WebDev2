using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AmberTurnerSite.Models
{
    public class Forum
    {
        private List<Reply> replies = new List<Reply>();        //list object

        //these become foreign keys, for db
        public int ForumID {get; set;} //

        [Required(ErrorMessage = "A page name is required")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "Page name must be between 3 and 20 characters")]
        public string PageName { get; set; }

        [Required(ErrorMessage = "A page rating is required")]
        [StringLength(1, MinimumLength = 1,
            ErrorMessage = "You must give a rating 1-5")]
        public string PageRating { get; set; }

        [Required(ErrorMessage = "Text is required to post")]
        [StringLength(200, MinimumLength = 10, 
            ErrorMessage = "A post must be at least 10 characters and no more than 200 characters")]
        public string PostText { get; set; }

        public AppUser PostCreator { get; set; }

        public DateTime PostDate { get; set; }

        public List<Reply> Replies 
        { 
            get { return replies; }             //reply object, w/n list object
        }
    }
}
