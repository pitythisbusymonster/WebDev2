using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberTurnerSite
{
    public class HorrorQuiz
    {
        public static string CheckAnswers(int qNumber, string answer)
        {
            string correct;
            switch (qNumber)
            {
                case 1:
                    correct = answer == "The Castle of Otranto" ? "Right" : "Wrong";
                    break;
                case 2:
                    correct = answer == "19" ? "Right" : "Wrong";
                    break;
                case 3:
                    correct = answer == "true" ? "Right" : "Wrong";
                    break;
                default:
                    correct = "Not a question";
                    break;
            }
            return correct;
        }

    }
}
