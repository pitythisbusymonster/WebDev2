using AmberTurnerSite.Models;
using AmberTurnerSite.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberTurnerSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostApiController : Controller
    {
        private readonly ForumRepository _repo;

        public PostApiController(ForumRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forum>>> GetPosts()
        {
            return await _repo.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Forum>> GetPosts(int id)
        {
            var cPost = await _repo.Posts.Where(u => u.ForumID == id).FirstOrDefaultAsync();  //FindAsync(id);

            if (cPost == null)
            {
                return NotFound();
            }

            return cPost;
        }

        [HttpPost]
        public async Task<ActionResult<Forum>> PostForumPost(Forum post)
        {
            await Task.Run(() => _repo.AddPost(post));
            //await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPosts), new { id = post.ForumID }, post);
        }
    }
}