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
    public class StatisticImgController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public StatisticImgController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            StatisticImg statisticImg = await _db.StatisticImg.FirstOrDefaultAsync();
            return View(statisticImg);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StatisticImg dbStatisticImg = await _db.StatisticImg.FirstOrDefaultAsync(x => x.Id == id);
            if (dbStatisticImg == null)
            {
                return BadRequest();
            }
            return View(dbStatisticImg);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, StatisticImg statisticImg)
        {
            if (id == null)
            {
                return NotFound();
            }
            StatisticImg dbStatisticImg = await _db.StatisticImg.FirstOrDefaultAsync(x => x.Id == id);
            if (dbStatisticImg == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbStatisticImg);
            }
            if (statisticImg.Photo != null)
            {
                if (!statisticImg.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                    return View(dbStatisticImg);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbStatisticImg.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbStatisticImg.Image = await statisticImg.Photo.SaveFileAsync(folder);
            }

            dbStatisticImg.Name = statisticImg.Name;
            dbStatisticImg.Title = statisticImg.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            StatisticImg statisticImg = await _db.StatisticImg.FirstOrDefaultAsync(x => x.Id == id);
            if (statisticImg == null)
            {
                return BadRequest();
            }
            return View(statisticImg);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StatisticImg statisticImg = await _db.StatisticImg.FirstOrDefaultAsync(x => x.Id == id);
            if (statisticImg == null)
            {
                return BadRequest();
            }
            if (statisticImg.IsDeactive)
            {
                statisticImg.IsDeactive = false;
            }
            else
            {
                statisticImg.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
