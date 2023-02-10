using Asan.DAL;
using Asan.Models;
using Asan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _db;

        public SearchController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (searchTerm == null)
            {
                return NotFound();
            }
            SearchsVM searchs = new SearchsVM
            {
                AboutSliders = await _db.AboutSliders.Where(p => p.Title.Contains(searchTerm)).ToListAsync(),
                Sliders = await _db.Sliders.Where(p => p.Title.Contains(searchTerm)).ToListAsync(),
                Contracts = await _db.Contracts.Where(p => p.Title.Contains(searchTerm)).ToListAsync(),
                Documents = await _db.Documents.Where(p => p.Title.Contains(searchTerm)).ToListAsync(),
                Legislations = await _db.Legislations.Include(x => x.LegislationCategory).Where(p => p.Title.Contains(searchTerm)).ToListAsync(),
                News = await _db.News.Where(p => p.Title.Contains(searchTerm)).ToListAsync()
            };
            if (searchs == null)
            {
                return RedirectToAction("Index");
            }
            return View(searchs);
        }
        public async Task<IActionResult> Create(Connect connect)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            connect.CreateTime = DateTime.UtcNow.AddHours(4);
            _db.Connects.Add(connect);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Search(string searchTerm)
        //{  // Veritabanındaki kayıtları ara ve sonuçları getir.

        //}
    }
}
