using Asan.DAL;
using Asan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }
        //[Route("Admin/Search")]
        //public IActionResult Search(string searchTerm)
        //{
        //    return ViewComponent("Search", new { searchTerm = searchTerm });
        //}

        //[HttpGet]
        //public async Task<IActionResult> Search(string searchTerm)
        //{
        //    //List<Legislation> legistration = await _db.Legislations.ToListAsync();

        //    ViewData["CurrentFilter"] = searchTerm;
        //    var legistration = from b in await _db.Legislations.ToListAsync() select b;
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        legistration = legistration.Where(b => b.Title.Contains(searchTerm));

        //    }
        //    return View(legistration);
        //}
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string searchTerm)
        {
            // Veritabanındaki kayıtları ara ve sonuçları getir.
            var searchResults = _db.Legislations.Include(x => x.LegislationCategory).Where(p => p.Title.Contains(searchTerm)).ToList();

            return View(searchResults);
        }
    }
}
