using Asan.DAL;
using Asan.Helpers;
using Asan.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class AboutSliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public AboutSliderController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<AboutSlider> aboutSliders = await _db.AboutSliders.ToListAsync();
            return View(aboutSliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutSlider aboutSlider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (aboutSlider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                return View();
            }

            if (!aboutSlider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin!");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            aboutSlider.Image = await aboutSlider.Photo.SaveFileAsync(folder);
            await _db.AboutSliders.AddAsync(aboutSlider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
           AboutSlider slider = await _db.AboutSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }
            return View(slider);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AboutSlider slider = await _db.AboutSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }
            if (slider.IsDeactive)
            {
                slider.IsDeactive = false;
            }
            else
            {
                slider.IsDeactive = true;
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
            AboutSlider dbSlider = await _db.AboutSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            return View(dbSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, AboutSlider slider)
        {

            if (id == null)
            {
                return NotFound();
            }
            AboutSlider dbSlider = await _db.AboutSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbSlider);
            }
            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error var");
                    return View(slider);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbSlider.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbSlider.Image = await slider.Photo.SaveFileAsync(folder);
            }
            dbSlider.Title = slider.Title;
            dbSlider.Description = slider.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
