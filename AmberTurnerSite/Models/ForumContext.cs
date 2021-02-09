using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AmberTurnerSite.Models
{
    public class ForumContext : IdentityDbContext
    {
        public ForumContext (
            DbContextOptions<ForumContext> options) : base(options) { }
        public DbSet<Forum> Posts { get; set; } //represent tables, in the db

        public DbSet<Reply> Replies { get; set; }

    }
}
