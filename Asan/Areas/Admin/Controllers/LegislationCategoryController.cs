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
    public class LegislationCategoryController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public LegislationCategoryController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<LegislationCategory> categories = await _db.LegislationCategory.ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LegislationCategory category, int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.LegislationCategory.AnyAsync(x => x.Title == category.Title);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            if (category.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                return View();
            }

            if (!category.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin!");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            category.Image = await category.Photo.SaveFileAsync(folder);
            await _db.LegislationCategory.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            LegislationCategory category = await _db.LegislationCategory.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            LegislationCategory category = await _db.LegislationCategory.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return BadRequest();
            }
            if (category.IsDeactive)
            {
                category.IsDeactive = false;
            }
            else
            {
                category.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LegislationCategory dbCategory = await _db.LegislationCategory.FirstOrDefaultAsync(x => x.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }

            return View(dbCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id , LegislationCategory category)
        {
            if (id == null)
            {
                return NotFound();
            }
            LegislationCategory dbCategory = await _db.LegislationCategory.FirstOrDefaultAsync(x => x.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbCategory);
            }
            if (category.Photo != null)
            {
                if (!category.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                    return View(dbCategory);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbCategory.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbCategory.Image = await category.Photo.SaveFileAsync(folder);
            }
            dbCategory.Title = category.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
