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
    public class GenneralInformationController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public GenneralInformationController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            GeneralInformation genneralInformations = await _db.GeneralInformation.FirstOrDefaultAsync();
            return View(genneralInformations);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GeneralInformation dbGenneralInformations = await _db.GeneralInformation.FirstOrDefaultAsync(x=>x.Id == id);
            if (dbGenneralInformations == null)
            {
                return BadRequest();
            }
            return View(dbGenneralInformations);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, GeneralInformation genneralInformation)
        {

            if (id == null)
            {
                return NotFound();
            }
            GeneralInformation dbGenneralInformations = await _db.GeneralInformation.FirstOrDefaultAsync(x => x.Id == id);
            if (dbGenneralInformations == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbGenneralInformations);
            }
            if (genneralInformation.Photo != null)
            {
                if (!genneralInformation.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Error var");
                    return View(genneralInformation);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbGenneralInformations.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbGenneralInformations.Image = await genneralInformation.Photo.SaveFileAsync(folder);
            }
            dbGenneralInformations.Title = genneralInformation.Title;
            dbGenneralInformations.Description = genneralInformation.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            GeneralInformation genneralInformations = await _db.GeneralInformation.FirstOrDefaultAsync(x => x.Id == id);
            if (genneralInformations == null)
            {
                return BadRequest();
            }
            return View(genneralInformations);
        }
        public async Task<IActionResult> Activity( int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GeneralInformation genneralInformation =await _db.GeneralInformation.FirstOrDefaultAsync(x => x.Id == id);
            if (genneralInformation == null )
            {
                return BadRequest();
            }
            if (genneralInformation.IsDeactive )
            {
                genneralInformation.IsDeactive = false;
            }
            else
            {
                genneralInformation.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
