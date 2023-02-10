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
    public class PenaltyCategoryController : Controller
    {
        private readonly AppDbContext _db;
        public PenaltyCategoryController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<PenaltyCategory> penaltyCategories = await _db.PenaltyCategories.ToListAsync();
            return View(penaltyCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PenaltyCategory penaltyCategories, int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.PenaltyCategories.AnyAsync(x => x.Title == penaltyCategories.Title);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            
            await _db.PenaltyCategories.AddAsync(penaltyCategories);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            PenaltyCategory penaltyCategories = await _db.PenaltyCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (penaltyCategories == null)
            {
                return BadRequest();
            }
            return View(penaltyCategories);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            PenaltyCategory penaltyCategories = await _db.PenaltyCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (penaltyCategories == null)
            {
                return BadRequest();
            }
            if (penaltyCategories.IsDeactive)
            {
                penaltyCategories.IsDeactive = false;
            }
            else
            {
                penaltyCategories.IsDeactive = true;
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
            PenaltyCategory dbPenaltyCategories = await _db.PenaltyCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPenaltyCategories == null)
            {
                return BadRequest();
            }

            return View(dbPenaltyCategories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, PenaltyCategory penaltyCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            PenaltyCategory dbPenaltyCategories = await _db.PenaltyCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPenaltyCategories == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbPenaltyCategories);
            }
            dbPenaltyCategories.Title = penaltyCategories.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}

