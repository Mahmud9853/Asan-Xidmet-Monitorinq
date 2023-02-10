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
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _db;

        public StatisticsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Statistic> statistic = await _db.Statistics.ToListAsync();
            return View(statistic);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (statistic.Title == null)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            if (statistic.Color == null)
            {
                ModelState.AddModelError("Color", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            if (statistic.Number == null)
            {
                ModelState.AddModelError("Color", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            await _db.Statistics.AddAsync(statistic);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Statistic dbStatic = await _db.Statistics.FirstOrDefaultAsync(x => x.Id == id);
            if (dbStatic == null)
            {
                return BadRequest();
            }
            return View(dbStatic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Statistic statistic)
        {

            if (id == null)
            {
                return NotFound();
            }
            Statistic dbStatic = await _db.Statistics.FirstOrDefaultAsync(x => x.Id == id);
            if (dbStatic == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbStatic);
            }
            dbStatic.Color = statistic.Color;
            dbStatic.Title = statistic.Title;
            dbStatic.Number = statistic.Number;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Statistic statistic = await _db.Statistics.FirstOrDefaultAsync(x => x.Id == id);
            if (statistic == null)
            {
                return BadRequest();
            }
            return View(statistic);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Statistic statistic = await _db.Statistics.FirstOrDefaultAsync(x => x.Id == id);
            if (statistic == null)
            {
                return BadRequest();
            }
            if (statistic.IsDeactive)
            {
                statistic.IsDeactive = false;
            }
            else
            {
                statistic.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
