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

        [Authorize] //this being present means only those logged in can have access
        public IActionResult Forum()       
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Forum(Forum model)
        {
            /* model.PostCreator = userManager.GetUserAsync(User).Result;
             model.PostCreator.Name = model.PostCreator.UserName; 
             model.PostDate = DateTime.Now;
             repo.AddPost(model);

             return View(model);*/

            if (ModelState.IsValid)
            {
                model.PostCreator = userManager.GetUserAsync(User).Result;
                model.PostCreator.Name = model.PostCreator.UserName;
                model.PostDate = DateTime.Now;
                repo.AddPost(model);
            }
            else
            {
                return RedirectToAction("Forum");
            }

            return RedirectToAction("ForumPosts");

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

        //open form for entering reply
        [Authorize] 
        public IActionResult Reply(int forumId)
        {
            var replyVM = new ReplyVM { ForumID = forumId };
            return View(replyVM);
        }


        [HttpPost]
        public RedirectToActionResult Reply(ReplyVM replyVM)  //IActionResult
        {
            //Reply is the domain model
            var reply = new Reply { ReplyText = replyVM.ReplyText };
            reply.Replier = userManager.GetUserAsync(User).Result;
            reply.ReplyDate = DateTime.Now;

            //retrieve the post replying to
            var post = (from r in repo.Posts
                        where r.ForumID == replyVM.ForumID
                        select r).First<Forum>();

            //store the reply with the post in the db
            post.Replies.Add(reply);
            repo.UpdatePost(post);

            //return View(reply);
            return RedirectToAction("ForumPosts");
        }


    }
}
