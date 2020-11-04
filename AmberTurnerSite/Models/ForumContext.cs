using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberTurnerSite.Models
{
    public class ForumContext : DbContext
    {
        public ForumContext (
            DbContextOptions<ForumContext> options) : base(options) { }
        public DbSet<Forum> Forum { get; set; }
        public DbSet<User> User { get; set; }



    }
}
