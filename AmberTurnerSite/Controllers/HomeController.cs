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
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        /*ForumContext context;//
        public HomeController(ForumContext c)//
        {
            context = c;
        }*/

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
            Forum model = new Forum();
            User uName = new User();
            model.PostCreator = uName;
            model.PostDate = DateTime.Now;

            return View(model);

            //return View();
        }

        [HttpPost]
        public IActionResult Forum(Forum model)
        {
            model.PostDate = DateTime.Now;

            //store the model in the db
            repo.AddPost(model);
            
            //is it not saveing to DB because no context.SaveChanges();  ?

            return View(model);
        }

        public IActionResult ForumPosts()//
        {
            //get all posts in the db
            List<Forum> posts = repo.Posts.ToList<Forum>();

            //var posts = context.Posts.Include(post => post.PostCreator).ToList<Forum>();  

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
