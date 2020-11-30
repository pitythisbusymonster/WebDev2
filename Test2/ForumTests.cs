using System;
using System.Linq;
using AmberTurnerSite.Controllers;
using AmberTurnerSite.Models;
using AmberTurnerSite.Repos;
using Xunit;


namespace Test2
{
    public class ForumTests
    {
        [Fact]
        public void AddForumTest()
        {
            
            // Arrange
            var fakeRepo = new FakeForumRepository();
            var controller = new HomeController(fakeRepo);
            var post = new Forum()
            {
                PageName = "Dracula",
                PageRating = "5 Stars",
                PostCreator = new User() { Name = "Me" },
                PostText = "I wish Stoker hadn't died before he saw the impact his book made"
            };

            // Act
            controller.Forum(post);

            // Assert
            // Ensure that the post was added to the repository
            var retrievedPost = fakeRepo.Posts.ToList()[0];
            Assert.Equal(0, System.DateTime.Now.Date.CompareTo(retrievedPost.PostDate.Date));
            Assert.Equal(post.PageName, retrievedPost.PageName);   //          
        }

        [Fact]
        public void GetForumTest()
        {

            // Arrange
            var fakeRepo = new FakeForumRepository();
            var controller = new HomeController(fakeRepo);
            var post = new Forum()
            {
                PageName = "The Willows",
                PageRating = "5 Stars",
                PostCreator = new User() { Name = "Me" },
                PostText = "So underrated!"
            };

            // Act
            controller.Forum(post);

            // Assert
            // Ensure that the post was added to the repository
            var retrievedPost = fakeRepo.Posts.ToList()[0];
            Assert.Equal(post.PostDate.Date, retrievedPost.PostDate.Date);
            Assert.Equal(post.PageName, retrievedPost.PageName);
            Assert.Equal(post.PageRating, retrievedPost.PageRating);
            Assert.Equal(post.PostCreator, retrievedPost.PostCreator);
            Assert.Equal(post.PostText, retrievedPost.PostText);
        }
    }
}
