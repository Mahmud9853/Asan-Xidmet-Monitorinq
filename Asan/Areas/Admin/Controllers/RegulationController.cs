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
    public class RegulationController : Controller
    {
        private readonly AppDbContext _db;

        public RegulationController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            Regulation regulation = await _db.Regulation.FirstOrDefaultAsync();
            return View(regulation);
        }
    
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Regulation dbRegulation = await _db.Regulation.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRegulation == null)
            {
                return BadRequest();
            }

            return View(dbRegulation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Regulation regulation)
        {
            ViewBag.PenaltyCategories = await _db.PenaltyCategories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Regulation dbRegulation = await _db.Regulation.FirstOrDefaultAsync(x => x.Id == id);
            if (dbRegulation == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbRegulation);
            }
            dbRegulation.Title = regulation.Title;
            dbRegulation.Description = regulation.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Regulation regulation = await _db.Regulation.FirstOrDefaultAsync(x => x.Id == id);
            if (regulation == null)
            {
                return BadRequest();
            }
            if (regulation.IsDeactive)
            {
                regulation.IsDeactive = false;
            }
            else
            {
                regulation.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Regulation regulation = await _db.Regulation.FirstOrDefaultAsync(x => x.Id == id);
            if (regulation == null)
            {
                return BadRequest();
            }

            return View(regulation);
        }
    }
}
