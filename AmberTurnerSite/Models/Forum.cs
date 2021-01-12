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

        [Required(ErrorMessage = "Text is required to post")]
        [StringLength(200, MinimumLength = 10)]
         public string PostText { get; set; }

        [Required]
        public User PostCreator { get; set; }

        public DateTime PostDate { get; set; }

        
    }
}
