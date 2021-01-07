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
        //these become foreign keys, for db
        public int ForumID {get; set;} //

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Title must be between 3 and 100 characters")]
        public string PageName { get; set; }

        public string PageRating { get; set; }

        [Required] 
        public string PostText { get; set; }

        [Required(ErrorMessage = "Poster name is required")]
        [StringLength(100, MinimumLength = 3)]
        public User PostCreator { get; set; }

        public DateTime PostDate { get; set; }

        
    }
}
