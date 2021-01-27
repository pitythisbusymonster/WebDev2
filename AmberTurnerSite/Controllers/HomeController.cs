using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AmberTurnerSite.Models;
using AmberTurnerSite.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AmberTurnerSite.Controllers
{
    public class HomeController : Controller
    {

        IPosts repo;
        UserManager<AppUser> userManager;

        public HomeController(IPosts r, UserManager<AppUser> u)
        {
            repo = r;
            userManager = u;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Forum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forum(Forum model)
        {
            model.PostCreator = userManager.GetUserAsync(User).Result;
            model.PostCreator.Name = model.PostCreator.UserName; 
            model.PostDate = DateTime.Now;
            repo.AddPost(model);

            return View(model);

            // TODO: get the user's real name in registration
        }

        public IActionResult ForumPosts()//
        {
            //get all posts in the db
            List<Forum> posts = repo.Posts.ToList<Forum>();

            //var posts = context.Posts.Include(post => post.PostCreator).ToList<Forum>();  

            return View(posts);
        }

        [HttpPost]
        public IActionResult ForumPosts(string pageName, string postCreator)
        {
            List<Forum> posts = null;

            if (pageName != null)
            {
                posts = (from r in repo.Posts
                           where r.PageName == pageName
                           select r).ToList();
            }
            else if (postCreator != null)
            {
                posts = (from r in repo.Posts
                           where r.PostCreator.Name == postCreator
                           select r).ToList();
            }

            return View(posts);
        }

        public IActionResult Overview()
        {
            return View();
        }

    }
}
