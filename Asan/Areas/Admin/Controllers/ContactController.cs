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
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ContactController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            Contact contacts = await _db.Contacts.FirstOrDefaultAsync();
            return View(contacts);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contact dbContacts = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbContacts == null)
            {
                return BadRequest();
            }

            return View(dbContacts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Contact contacts)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contact dbContacts = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbContacts == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbContacts);
            }
            if (contacts.Photo != null)
            {
                if (!contacts.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zəhmət olmasa şəkil seçin!");
                    return View(dbContacts);
                }
                string folder = Path.Combine(_env.WebRootPath, "img");
                string path = Path.Combine(folder, dbContacts.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbContacts.Image = await contacts.Photo.SaveFileAsync(folder);
            }
            dbContacts.PhoneNumber = contacts.PhoneNumber;
            dbContacts.PhoneCenter = contacts.PhoneCenter;
            dbContacts.PhoneCenterSvg = contacts.PhoneCenterSvg;
            dbContacts.EmailCenter = contacts.EmailCenter;
            dbContacts.EmailAsan = contacts.EmailAsan;
            dbContacts.LocationFirst = contacts.LocationFirst;
            dbContacts.LocationTwo = contacts.LocationTwo;
            dbContacts.PhoneSvg = contacts.PhoneSvg;
            dbContacts.EmailCenterSvg = contacts.EmailCenterSvg;
            dbContacts.EmailAsanSvg = contacts.EmailAsanSvg;
            dbContacts.LocationSvgFirst = contacts.LocationSvgFirst;
            dbContacts.LocationSvgTwo = contacts.LocationSvgTwo;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contact contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact == null)
            {
                return BadRequest();
            }
            return View(contact);
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contact contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact == null)
            {
                return BadRequest();
            }
            if (contact.IsDeactive)
            {
                contact.IsDeactive = false;
            }
            else
            {
                contact.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
