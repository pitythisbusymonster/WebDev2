using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AmberTurnerSite.Models
{
    public class ForumContext : IdentityDbContext
    {
        public ForumContext (
            DbContextOptions<ForumContext> options) : base(options) { }
        public DbSet<Forum> Posts { get; set; } //these represent tables, in the db //changed from Forum to Posts 

    }
}
