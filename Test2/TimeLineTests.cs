using AmberTurnerSite.Models;
using System;
using Xunit;

namespace Test2
{
    
    public class TimeLineTests
    {
        [Fact]
        public void RightAnswerTest()
        {
            //Arrange
            var quiz = new QuizVM() { 
                UserAnswer1 = "The Castle of Otranto", 
                UserAnswer2 = "19", 
                UserAnswer3 = "true" };

            //Act
            quiz.CheckAnswers();

            //Assert
            Assert.True(
                "Right" == quiz.RightWrong1 && 
                "Right" == quiz.RightWrong2 && 
                "Right" == quiz.RightWrong3);
        }

        [Fact]
        public void WrongAnswerTest()
        {
            //Arrange
            var quiz = new QuizVM()
            {
                UserAnswer1 = "The Case of Otranto",
                UserAnswer2 = "20",
                UserAnswer3 = "false"
            };

            //Act
            quiz.CheckAnswers();

            //Assert
            Assert.True(
                "Wrong" == quiz.RightWrong1 &&
                "Wrong" == quiz.RightWrong2 &&
                "Wrong" == quiz.RightWrong3);


        }
    }
}
