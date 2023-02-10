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
    public class NewsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public NewsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<News> news = await _db.News.Where(x => x.IsDeactive).ToListAsync();
            return View(news);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (news.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                return View();
            }

            if (!news.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zəhmət olmasa bir şəkil seçin!");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img");
            news.Image = await news.Photo.SaveFileAsync(folder);
            await _db.News.AddAsync(news);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            News dbNews = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (dbNews == null)
            {
                return BadRequest();
            }
            return View(dbNews);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, News news)
        {

            if (id == null)
            {
                return NotFound();
            }
            News dbNews = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (dbNews == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbNews);
            }
            if (news.Photo != null)
            {
                if (!news.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error var");
                    return View(news);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbNews.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbNews.Image = await news.Photo.SaveFileAsync(folder);
            }
            dbNews.Title = news.Title;
            dbNews.Description = news.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> Detail(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();

        //    }
        //    News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
        //    if (news == null)
        //    {
        //        return BadRequest();
        //    }
        //    return View(news);
        //}
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                    return NotFound();

            }
            News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (news == null)
            {
                return BadRequest();
            }
            return PartialView(news);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (news == null)
            {
                return BadRequest();

            }
            return View(news);

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
            News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (news == null)
            {
                return BadRequest();
            }
            news.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
