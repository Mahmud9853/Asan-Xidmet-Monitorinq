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
    public class PenaltyController : Controller
    {
        private readonly AppDbContext _db;

        public PenaltyController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<PenaltyCategory> penaltyCategory = await _db.PenaltyCategories.Include(x => x.Penalty).Where(x => x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
            List<Penalty> penalty = await _db.Penalties.Include(x => x.PenaltyCategory).Where(x => x.IsDeactive).ToListAsync();
            List<Fine> fines = await _db.Fines.Include(x => x.Category).Where(x => x.IsDeactive).ToListAsync();
            List<Category> Category = await _db.Categories.Where(x => x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
            PenaltyVM penaltyVM = new PenaltyVM
            {
                Penalty = penalty,
                PenaltyCategory = penaltyCategory,
                Category = Category,
                Fines=fines
                
            };
            return View(penaltyVM);
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Connect connect)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            connect.CreateTime = DateTime.UtcNow.AddHours(4);
            _db.Connects.Add(connect);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
