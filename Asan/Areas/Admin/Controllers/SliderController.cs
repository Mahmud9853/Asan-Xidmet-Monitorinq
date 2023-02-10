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
    public class SliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo" ,"Zəhmət olmasa şəkil seçin!");
                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin!");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            slider.Image = await slider.Photo.SaveFileAsync(folder);
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();

            }
            return View(slider);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();

            }
            _db.Sliders.Remove(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            return View(dbSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {

            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
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
                    return View(dbSlider);
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
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }
            return View(slider);
        }

    }
}
