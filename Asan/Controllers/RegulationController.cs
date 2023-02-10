using Asan.DAL;
using Asan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Controllers
{
    public class RegulationController : Controller
    {
        private readonly AppDbContext _db;

        public RegulationController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            Regulation regulation = await _db.Regulation.Where(x => x.IsDeactive).FirstOrDefaultAsync();
            return View(regulation);
           
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
            return RedirectToAction("Index");
        }
    }
}
