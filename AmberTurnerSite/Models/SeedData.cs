using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AmberTurnerSite.Models
{
    public class SeedData
    {
        public static void Seed(ForumContext context)
        {
            if (!context.Posts.Any())
            {
                Forum post = new Forum
                {
                    PageName = "The Monk",
                    PageRating = "5",
                    PostText = "Great book, a must read!",
                    PostCreator = new AppUser { Name = "Shel Silverstein" },
                    PostDate = DateTime.Parse("11/1/2020")
                };

                context.Posts.Add(post);

                post = new Forum
                {
                    PageName = "The Monk",
                    PageRating = "5",
                    PostText = "So creepy, even for a modern reader",
                    PostCreator = new AppUser { Name = "George Eliot" },
                    PostDate = DateTime.Parse("11/12/2020")
                };

                context.Posts.Add(post);

                // My next two reviews will be by the same user, so I will create
                // the user object once and store it so that both reviews will be
                // associated with the same entity in the DB.

                AppUser posterSarahCrew = new AppUser() { Name = "Sarah Crew" };
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


                context.SaveChanges(); // stores all the reviews in the DB

            }
        }
    }
}
