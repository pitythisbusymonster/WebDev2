using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmberTurnerSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmberTurnerSite.Controllers
{
    public class TimeLineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NoteableWorks()
        {
            return View();
        }

        public IActionResult Quiz()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Quiz(QuizVM quiz)
        {
            quiz.CheckAnswers();
            return View(quiz);
        }


    }
}
