using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AmberTurnerSite.Models
{
    public class AppUser : IdentityUser  //changed user to appUser, did a Rename
    {
        //public int UserID { get; set; } //remove, because conflists with Identity inheritence

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
    }
}
