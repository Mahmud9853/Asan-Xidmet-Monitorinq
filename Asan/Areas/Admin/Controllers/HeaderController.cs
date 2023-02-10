using Asan.DAL;
using Asan.Helpers;
using Asan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeaderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public HeaderController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            Header header = await _db.Headers.FirstOrDefaultAsync();
            return View(header);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Header dbHeaders = await _db.Headers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbHeaders == null)
            {
                return BadRequest();
            }
            return View(dbHeaders);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Header header)
        {

            if (id == null)
            {
                return NotFound();
            }
            Header dbHeaders = await _db.Headers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbHeaders == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbHeaders);
            }
            if (header.Photo != null)
            {
                if (!header.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error var");
                    return View(header);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbHeaders.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbHeaders.Image = await header.Photo.SaveFileAsync(folder);
            }
            dbHeaders.Title = header.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Header header = await _db.Headers.FirstOrDefaultAsync(x => x.Id == id);
            if (header == null)
            {
                return BadRequest();
            }
            return View(header);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Header header = await _db.Headers.FirstOrDefaultAsync(x => x.Id == id);
            if (header == null)
            {
                return BadRequest();
            }
            if (header.IsDeactive)
            {
                header.IsDeactive = false;
            }
            else
            {
                header.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
