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
    public class PenaltyController : Controller
    {
        private readonly AppDbContext _db;
  

        public PenaltyController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Penalty> penalty = await _db.Penalties.Include(x => x.PenaltyCategory).OrderByDescending(x => x.Id).ToListAsync();
            return View(penalty);
         
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.PenaltyCategories = await _db.PenaltyCategories.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Penalty penalty, int categoryId)
        {
            ViewBag.PenaltyCategories = await _db.PenaltyCategories.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.Penalties.AnyAsync(x => x.Text == penalty.Text);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Error Name");
                return View();

            }
            penalty.PenaltyCategoryId = categoryId;
            await _db.Penalties.AddAsync(penalty);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Penalty penalty = await _db.Penalties.Include(x => x.PenaltyCategory).FirstOrDefaultAsync(x => x.Id == id);
            if (penalty == null)
            {
                return BadRequest();
            }
            return View(penalty);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            Penalty penalty = await _db.Penalties.FirstOrDefaultAsync(x => x.Id == id);
            if (penalty == null)
            {
                return BadRequest();
            }
            if (penalty.IsDeactive)
            {
                penalty.IsDeactive = false;
            }
            else
            {
                penalty.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.PenaltyCategories = await _db.PenaltyCategories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Penalty dbPenalty = await _db.Penalties.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPenalty == null)
            {
                return BadRequest();
            }

            return View(dbPenalty);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Penalty penalty, int penaltyCategoryId)
        {
            ViewBag.PenaltyCategories = await _db.PenaltyCategories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Penalty dbPenalty = await _db.Penalties.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPenalty == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbPenalty);
            }
            dbPenalty.Text = penalty.Text;
            dbPenalty.PenaltyCategoryId = penaltyCategoryId;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
