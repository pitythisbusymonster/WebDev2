using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
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
