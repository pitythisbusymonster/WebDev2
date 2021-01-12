using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.Id

namespace AmberTurnerSite.Models
{
    public class ForumContext : DbContext
    {
        public ForumContext (
            DbContextOptions<ForumContext> options) : base(options) { }
        public DbSet<Forum> Posts { get; set; } //these represent tables, in the db //changed from Forum to Posts
        public DbSet<User> Users { get; set; }   //changed to <User> Users

    }
}
