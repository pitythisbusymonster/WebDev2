using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AmberTurnerSite.Models
{
    public class SeedData
    {
        public static void Seed(ForumContext context, UserManager<AppUser> userManager, 
                                    RoleManager<IdentityRole> roleManager)
        {
            if (!context.Posts.Any())
            {
                var result = roleManager.CreateAsync(new IdentityRole("Member")).Result;
                result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;

                //seeding default admin. they will need to change password after logging in
                AppUser siteadmin = new AppUser
                { 
                    UserName = "SiteAdmin" ,
                    Name = "Site Admin"
                };
                userManager.CreateAsync(siteadmin, "Password-01").Wait();
                IdentityRole adminRole = roleManager.FindByNameAsync("Admin").Result;
                userManager.AddToRoleAsync(siteadmin, adminRole.Name);

                //seed users and reviews for manual testing

                AppUser shelSilverstein = new AppUser{
                    UserName = "SilverShel", Name = "S. Silverstein"};
                context.Users.Add(shelSilverstein);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                Forum post = new Forum
                {
                    PageName = "The Monk",
                    PageRating = "5",
                    PostText = "Great book, a must read!",
                    PostCreator = shelSilverstein,
                    PostDate = DateTime.Parse("11/1/2020")
                };

                context.Posts.Add(post);

                AppUser gEliot = new AppUser
                {
                    UserName = "georgeEliot",
                    Name = "George Eliot"
                };
                context.Users.Add(gEliot);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                post = new Forum
                {
                    PageName = "The Monk",
                    PageRating = "5",
                    PostText = "So creepy, even for a modern reader",
                    PostCreator = gEliot,
                    PostDate = DateTime.Parse("11/12/2020")
                };

                context.Posts.Add(post);

                // My next two reviews will be by the same user, so I will create
                // the user object once and store it so that both reviews will be
                // associated with the same entity in the DB.

                AppUser posterSarahCrew = new AppUser() { UserName = "LittlePrincess", Name = "Sarah Crew" };
                context.Users.Add(posterSarahCrew);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                post = new Forum
                {
                    PageName = "NOS4A2",
                    PageRating = "5",
                    PostText = "Joe Hill is a great writer on his own merit",
                    PostCreator = posterSarahCrew,
                    PostDate = DateTime.Parse("11/15/2020")
                };
                context.Posts.Add(post);

                post = new Forum
                {
                    PageName = "Algernon Blackwood",
                    PageRating = "5",
                    PostText = "",
                    PostCreator = posterSarahCrew,
                    PostDate = DateTime.Parse("11/11/2020")
                };
                context.Posts.Add(post);

                //testing an issue
                
                AppUser posterAmber = new AppUser() { UserName = "itsMe", Name = "Amber" };
                context.Users.Add(posterAmber);
                context.SaveChanges();   // This will add a UserID to the reviewer object

                post = new Forum
                {
                    PageName = "King vs Bachman",
                    PageRating = "4",
                    PostText = "What do you think of his Bachman books?",
                    PostCreator = posterAmber,
                    PostDate = DateTime.Parse("1/21/2020")
                };
                context.Posts.Add(post);

                /*post = new Forum
                {
                    PageName = "Scary Stories to Tell in the Dark",
                    PageRating = "4",
                    PostText = "What do you guys think about the recent movie based on the children's classic?",
                    PostCreator = posterAmber,
                    PostDate = DateTime.Parse("1/30/2020")
                };
                context.Posts.Add(post);*/


                context.SaveChanges(); // stores all the reviews in the DB

            }
        }
    }
}
