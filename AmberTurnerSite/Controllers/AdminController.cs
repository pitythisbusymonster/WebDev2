using Microsoft.AspNetCore.Mvc;
using AmberTurnerSite.Repos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AmberTurnerSite.Models;

namespace AmberTurnerSite.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IPosts postRepo;    //

        public AdminController(UserManager<AppUser> userMngr,
                                RoleManager<IdentityRole> roleMngr,
                                IPosts repo)    //
        {
            userManager = userMngr;
            roleManager = roleMngr;
            postRepo = repo;        //
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>();
            foreach (AppUser user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);//
                users.Add(user);
            }

            //this is an initilization list
            AdminVM model = new AdminVM{
                Users = users,              //list of AppUsers
                Roles = roleManager.Roles
            };      

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityResult result = null;
            AppUser user = await userManager.FindByIdAsync(id); 
            if (user != null)
            {
                // Check to see if the user has posted a review
                if (0 == (from r in postRepo.Posts
                          where r.PostCreator.Name == user.Name
                          select r).Count<Forum>())
                {

                    result = await userManager.DeleteAsync(user);
                }
                else
                {
                    result = IdentityResult.Failed(new IdentityError() 
                    { Description = "User's reviews must be deleted first" });
                }

                if (!result.Succeeded)
                { 
                    // if failed 
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += errorMessage != "" ? " | " : "";   // put a separator between messages
                        errorMessage += error.Description ;
                    }
                    TempData["message"] = errorMessage;
                }
                else
                {
                    TempData["message"] = "";  // No errors, clear the message
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin"); 
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " 
                    + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id); 
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            var result = await userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }


        // Role management 

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterVM model)
        {

            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
         
    }
}
