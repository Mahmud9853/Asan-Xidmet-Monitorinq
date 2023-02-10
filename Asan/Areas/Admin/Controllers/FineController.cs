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
    public class FineController : Controller
    {
        private readonly AppDbContext _db;
        public FineController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Fine> fine = await _db.Fines.Include(x=>x.Category).ToListAsync();
            return View(fine);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.FineCategory = await _db.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fine fine, int categoryId)
        {
            ViewBag.FineCategory = await _db.Categories.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.Fines.AnyAsync(x => x.Text == fine.Text);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Error Name");
                return View();

            }
            fine.CategoryId = categoryId;
            await _db.Fines.AddAsync(fine);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Fine fine = await _db.Fines.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (fine == null)
            {
                return BadRequest();
            }
            return View(fine);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            Fine fine = await _db.Fines.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (fine == null)
            {
                return BadRequest();
            }
            if (fine.IsDeactive)
            {
                fine.IsDeactive = false;
            }
            else
            {
                fine.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.FineCategory = await _db.Categories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Fine dbFine = await _db.Fines.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (dbFine == null)
            {
                return BadRequest();
            }

            return View(dbFine);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Fine fine, int CategoryId)
        {
            ViewBag.FineCategory = await _db.Categories.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Fine dbFine = await _db.Fines.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (dbFine == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbFine);
            }
            dbFine.Text = fine.Text;
            dbFine.CategoryId = CategoryId;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
