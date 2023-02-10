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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _db.Categories.Include(x => x.Fines).ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = await _db.Categories.AnyAsync(x => x.Title == category.Title);
            if (IsExist)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xananı doldurun !");
                return View();
            }

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Category category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
           Category category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return BadRequest();
            }
            if (category.IsDeactive)
            {
                category.IsDeactive = false;
            }
            else
            {
                category.IsDeactive = true;
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
            Category dbCategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }

            return View(dbCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category dbCategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbCategory == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbCategory);
            }
            dbCategory.Title = category.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
