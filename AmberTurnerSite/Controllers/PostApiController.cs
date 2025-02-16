﻿using AmberTurnerSite.Models;
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
    public class PostApiController : ControllerBase
    {

        private readonly IPosts repo;


        public PostApiController(IPosts r)
        {
            repo = r;
        }

        public IActionResult Get()
        {
            var replies = repo.Replies.ToList<Reply>();
            if (replies.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(replies);
            }
        }

        /*[HttpPost]
        public async Task<ActionResult<Reply>> PostReply(Reply reply)
        {
            await Task.Run(() => repo.AddReply(reply));
            //await repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = reply.ReplyID }, reply);
        }*/


        //


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forum>>> GetPosts()
        {
            return await repo.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Forum>> GetPost(int id)
        {
            var post = await repo.Posts.Where(u => u.ForumID == id).FirstOrDefaultAsync();

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpPost]
        public async Task<ActionResult<Forum>> PostForumPost(Forum post)
        {
            await Task.Run(() => repo.AddPost(post));
            //await repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.ForumID }, post);
        }







    }
}
