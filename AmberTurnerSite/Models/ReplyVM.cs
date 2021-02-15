using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmberTurnerSite.Models
{
    public class ReplyVM
    {
        public int ForumID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string PageName { get; set; }
        public string ReplyText { get; set; }
    }
}
