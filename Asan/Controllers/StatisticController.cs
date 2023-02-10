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
    public class StatisticController : Controller
    {
        private readonly AppDbContext _db;

        public StatisticController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            StatisticVM statisticVM = new StatisticVM
            {
                Statics = await _db.Statistic.Where(x=>x.IsDeactive).ToListAsync(),
                Statistics = await _db.Statistics.Where(x => x.IsDeactive).ToListAsync(),
                StatisticImg = await _db.StatisticImg.Where(x => x.IsDeactive).FirstOrDefaultAsync()
            };
            return View(statisticVM);
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
