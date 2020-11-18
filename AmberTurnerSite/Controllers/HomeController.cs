using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AmberTurnerSite.Models;
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

        ForumContext context;//
        public HomeController(ForumContext c)//
        {
            context = c;
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
            //model.PostDate = DateTime.Now;
            return View(model);

            //return View();
        }

        [HttpPost]
        public IActionResult Forum(Forum model)
        {
            model.PostDate = DateTime.Now;
            //store the model in the db
            context.Forum.Add(model);   //ref to line 13 in ForumContext (if change there, change here and line 57
            context.SaveChanges();

            return View(model);
        }

        public IActionResult Forums()//
        {
            var forumPosts = context.Forum.Include(post => post.PostCreator).ToList<Forum>();  

            return View(forumPosts);
        }

        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
