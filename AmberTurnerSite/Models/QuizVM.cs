using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberTurnerSite.Models
{
    public class QuizVM
    {
        //user answers and whether right or wrong for 3 questions
        public String UserAnswer1 { get; set; }
        public String RightWrong1 { get; set; }  //could make bool

        public String UserAnswer2 { get; set; }
        public String RightWrong2 { get; set; }

        public String UserAnswer3 { get; set; }
        public String RightWrong3 { get; set; }

        //checks each answer to see it it's right or wrong, returned in string
        //this is a stub
        public void CheckAnswers()
        {
            RightWrong1 = UserAnswer1 == "The Castle of Otranto" ? "Right" : "Wrong";
            RightWrong2 = UserAnswer2 == "19" ? "Right" : "Wrong";
            RightWrong3 = UserAnswer3 == "true" ? "Right" : "Wrong";
        }


    }
}
