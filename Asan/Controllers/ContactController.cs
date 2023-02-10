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
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;

        public ContactController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            Contact contacts = await _db.Contacts.Where(x => x.IsDeactive).FirstOrDefaultAsync();
            return View(contacts);
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
