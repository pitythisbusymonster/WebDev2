using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AmberTurnerSite.Models
{
    public class User       
    {
        public int UserID { get; set; } //

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
    }
}
