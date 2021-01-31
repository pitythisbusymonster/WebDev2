using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmberTurnerSite.Models
{
    public class AppUser : IdentityUser  //changed user to appUser, did a Rename
    {
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Your name must be between 3 and 15 characters")]
        [Required]
        public string Name { get; set; }

        [NotMapped]   //means it won't be added to db, so no migration needed
        public IList<string> RoleNames { get; set; }
    }
}
