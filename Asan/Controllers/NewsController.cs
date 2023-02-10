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
    public class NewsController : Controller
    {
        private readonly AppDbContext _db;

        public NewsController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<News> news =await _db.News.Where(x=>x.IsDeactive).ToListAsync();
            return View(news);
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
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            News news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (news == null)
            {
                return BadRequest();
            }
            return View(news);
        }
    }
}
