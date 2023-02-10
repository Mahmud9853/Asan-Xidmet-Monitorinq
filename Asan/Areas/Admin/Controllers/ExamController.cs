using Asan.DAL;
using Asan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExamController : Controller
    {
        private readonly AppDbContext _db;

        public ExamController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Exam> exams = await _db.Exams.ToListAsync();
            return View(exams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (exam.TitleFirst == null)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            if (exam.SvgFirst == null)
            {
                ModelState.AddModelError("Svg", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            await _db.Exams.AddAsync(exam);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Exam dbExam = await _db.Exams.FirstOrDefaultAsync(x => x.Id == id);
            if (dbExam == null)
            {
                return BadRequest();
            }
            return View(dbExam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Exam exam)
        {
            if (id == null)
            {
                return NotFound();
            }
            Exam dbExam = await _db.Exams.FirstOrDefaultAsync(x => x.Id == id);
            if (dbExam == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbExam);
            }
            dbExam.SvgFirst = exam.SvgFirst;
            dbExam.SvgSecond = exam.SvgSecond;
            dbExam.TitleSecond = exam.TitleSecond;
            dbExam.TitleFirst = exam.TitleFirst;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Exam exam = await _db.Exams.FirstOrDefaultAsync(x => x.Id == id);
            if (exam == null)
            {
                return BadRequest();
            }
            return View(exam);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Exam exam = await _db.Exams.FirstOrDefaultAsync(x => x.Id == id);
            if (exam == null)
            {
                return BadRequest();
            }
            if (exam.IsDeactive)
            {
                exam.IsDeactive = false;
            }
            else
            {
                exam.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
