using Asan.DAL;
using Asan.Helpers;
using Asan.Models;
using Asan.ViewModels;
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
    public class LegislationController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public LegislationController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Legislation> legislations = await _db.Legislations.Include(x => x.LegislationCategory).OrderByDescending(x=>x.Id).ToListAsync();
            return View(legislations);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.LegislationCategories = await _db.LegislationCategory.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Legislation legislation, int categoryId)
        {
            ViewBag.Categories = await _db.LegislationCategory.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.Legislations.AnyAsync(x => x.Title == legislation.Title);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Error Name");
                return View();

            }
            if (legislation.File == null)
            {
                ModelState.AddModelError("Document", "Error");
                return View();
            }

            if (!legislation.File.IsDocument())
            {
                ModelState.AddModelError("Document", "Error var");
                return View();
            }
            //if (!file.Photo.IsOlder1Mb())
            //{
            //    ModelState.AddModelError("Photo", "MAX 5Mb");
            //    return View();
            //}
            string folder = Path.Combine(_env.WebRootPath, "File");
            legislation.Attachment = await legislation.File.SaveFileAsync(folder);
            legislation.LegislationCategoryId = categoryId;
            await _db.Legislations.AddAsync(legislation);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Legislation legislation = await _db.Legislations.Include(x=>x.LegislationCategory).FirstOrDefaultAsync(x => x.Id == id);
            if (legislation == null)
            {
                return BadRequest();
            }
            return View(legislation);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            Legislation legislation = await _db.Legislations.FirstOrDefaultAsync(x => x.Id == id);
            if (legislation == null)
            {
                return BadRequest();
            }
            if (legislation.IsDeactive)
            {
                legislation.IsDeactive = false;
            }
            else
            {
                legislation.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.LegislationCategory.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Legislation dbLegislation = await _db.Legislations.FirstOrDefaultAsync(x => x.Id == id);
            if (dbLegislation == null)
            {
                return BadRequest();
            }

            return View(dbLegislation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Legislation legislation, int legislationCategoryId)
        {
            ViewBag.Categories = await _db.LegislationCategory.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Legislation dbLegislation = await _db.Legislations.FirstOrDefaultAsync(x => x.Id == id);
            if (dbLegislation == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbLegislation);
            }
            if (legislation.File != null)
            {
                if (!legislation.File.IsDocument())
                {
                    ModelState.AddModelError("File", "Zəhmət olmasa sənəd seçin!");
                    return View(dbLegislation);
                }
                string folder = Path.Combine(_env.WebRootPath, "File");
                string path = Path.Combine(folder, dbLegislation.Attachment);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbLegislation.Attachment = await legislation.File.SaveFileAsync(folder);
            }
            dbLegislation.Title = legislation.Title;
            dbLegislation.Svg = legislation.Svg;
            dbLegislation.LegislationCategoryId = legislationCategoryId;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public IActionResult Search()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Search(string key)
        //{
        //    List<Legislation> legislations = await _db.Legislations.Where(x => x.Title.Contains(key)).ToListAsync();
        //    return View( legislations);
        //}
        //public async Task<IActionResult> Search(SearcVM model)
        //{
        //    List<Legislation> legislations = await _db.Legislations.Where(x => x.Title.Contains(model.SearchTerm)).ToListAsync();

        //    return View(legislations);
        //}

    }
}
