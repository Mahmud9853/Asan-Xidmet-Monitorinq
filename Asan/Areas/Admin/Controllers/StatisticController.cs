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
    public class StatisticController : Controller
    {
        private readonly AppDbContext _db;

        public StatisticController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Static> statics = await _db.Statistic.ToListAsync();
            return View(statics);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Static statics)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (statics.Title == null)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            if (statics.Color == null)
            {
                ModelState.AddModelError("Color", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            if (statics.Number == null)
            {
                ModelState.AddModelError("Color", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            await _db.Statistic.AddAsync(statics);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Static dbStatic = await _db.Statistic.FirstOrDefaultAsync(x => x.Id == id);
            if (dbStatic == null)
            {
                return BadRequest();
            }
            return View(dbStatic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Static statics)
        {

            if (id == null)
            {
                return NotFound();
            }
            Static dbStatic = await _db.Statistic.FirstOrDefaultAsync(x => x.Id == id);
            if (dbStatic == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbStatic);
            }
            dbStatic.Color = statics.Color;
            dbStatic.Title = statics.Title;
            dbStatic.Number = statics.Number;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Static statics = await _db.Statistic.FirstOrDefaultAsync(x => x.Id == id);
            if (statics == null)
            {
                return BadRequest();
            }
            return View(statics);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Static statics = await _db.Statistic.FirstOrDefaultAsync(x => x.Id == id);
            if (statics == null)
            {
                return BadRequest();
            }
            if (statics.IsDeactive)
            {
                statics.IsDeactive = false;
            }
            else
            {
                statics.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
