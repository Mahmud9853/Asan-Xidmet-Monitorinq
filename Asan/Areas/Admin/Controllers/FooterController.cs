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
    public class FooterController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public FooterController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Information> ınformations = await _db.Informations.ToListAsync();
            return View(ınformations);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Information information)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (information.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                return View();
            }

            if (!information.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin!");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            information.Image = await information.Photo.SaveFileAsync(folder);
            await _db.Informations.AddAsync(information);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Information dbInformations = await _db.Informations.FirstOrDefaultAsync(x => x.Id == id);
            if (dbInformations == null)
            {
                return BadRequest();
            }
            return View(dbInformations);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Information informations)
        {

            if (id == null)
            {
                return NotFound();
            }
            Information dbInformations = await _db.Informations.FirstOrDefaultAsync(x => x.Id == id);
            if (dbInformations == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbInformations);
            }
            if (informations.Photo != null)
            {
                if (!informations.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error var");
                    return View(informations);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbInformations.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbInformations.Image = await informations.Photo.SaveFileAsync(folder);
            }
            dbInformations.Title = informations.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Information informations = await _db.Informations.FirstOrDefaultAsync(x => x.Id == id);
            if (informations == null)
            {
                return BadRequest();
            }
            return View(informations);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Information informations = await _db.Informations.FirstOrDefaultAsync(x => x.Id == id);
            if (informations == null)
            {
                return BadRequest();
            }
            if (informations.IsDeactive)
            {
                informations.IsDeactive = false;
            }
            else
            {
                informations.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
