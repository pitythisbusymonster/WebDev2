using System;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
using AmberTurnerSite.Models;
using AmberTurnerSite.Repos;
using Microsoft.EntityFrameworkCore;

namespace AmberTurnerSite.Controllers
{
    public class HomeController : Controller
    {

        IPosts repo;

        public HomeController(IPosts r)//
        {
            repo = r;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forum()
        {
            /*Forum model = new Forum();
            User uName = new User();
            model.PostCreator = uName;
            model.PostDate = DateTime.Now;

            return View(model);*/

            return View();
        }

        [HttpPost]
        public IActionResult Forum(Forum model)
        {
            /*model.PostDate = DateTime.Now;
            //store the model in the db
            repo.AddPost(model);*/

            if (ModelState.IsValid)
            {
                model.PostDate = DateTime.Now;
                // Store the model in the database
                repo.AddPost(model);
            }

            return View(model);
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
            /*var posts = (from r in repo.Posts
                           where r.PageName == pageName
                           select r).ToList();*/
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

        public IActionResult Privacy()
        {
            return View();
        }


        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
