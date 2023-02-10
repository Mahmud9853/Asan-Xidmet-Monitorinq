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
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;

        public AboutController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> Download(int? id)
        {
            Legislation  legislation = await _db.Legislations.FirstOrDefaultAsync(x => x.Id == id);
            if (legislation == null)
            {
                return NotFound();
            }

            string fileBytes = $"~/File/" + legislation.Attachment;

            return File(
                fileBytes,         /*string*/
                "application/pdf", /*mime type*/
                "fileName.pdf" /*name of the file (bax)*/
            );

        }
    }
}
