using Asan.DAL;
using Asan.Helpers;
using Asan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuizController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public QuizController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Quiz> quizs = await _db.Quizs.ToListAsync();
            return View(quizs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (quiz.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                return View();
            }

            if (!quiz.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin!");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            quiz.Image = await quiz.Photo.SaveFileAsync(folder);
            await _db.Quizs.AddAsync(quiz);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Quiz dbQuiz = await _db.Quizs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbQuiz == null)
            {
                return BadRequest();
            }
            return View(dbQuiz);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Quiz quiz)
        {

            if (id == null)
            {
                return NotFound();
            }
            Quiz dbQuiz = await _db.Quizs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbQuiz == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbQuiz);
            }
            if (quiz.Photo != null)
            {
                if (!quiz.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error var");
                    return View(dbQuiz);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbQuiz.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbQuiz.Image = await quiz.Photo.SaveFileAsync(folder);
            }
            dbQuiz.Number = quiz.Number;
            dbQuiz.Answer = quiz.Answer;
            dbQuiz.Question = quiz.Question;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Quiz quiz = await _db.Quizs.FirstOrDefaultAsync(x => x.Id == id);
            if (quiz == null)
            {
                return BadRequest();
            }
            if (quiz.IsDeactive)
            {
                quiz.IsDeactive = false;
            }
            else
            {
                quiz.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Quiz quiz = await _db.Quizs.FirstOrDefaultAsync(x => x.Id == id);
            if (quiz == null)
            {
                return BadRequest();
            }
            return View(quiz);
        }

    }
}
