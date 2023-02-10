using Asan.DAL;
using Asan.Helpers;
using Asan.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AccountController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Account> accounts =await _db.Accounts.ToListAsync();
            return View(accounts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (account.File == null)
            {
                ModelState.AddModelError("File", "Error");
                return View();
            }

            if (!account.File.IsDocument())
            {
                ModelState.AddModelError("File", "Error var");
                return View();
            }
            //if (!file.Photo.IsOlder1Mb())
            //{
            //    ModelState.AddModelError("Photo", "MAX 5Mb");
            //    return View();
            //}
            string folder = Path.Combine(_env.WebRootPath, "File");
            account.Attachment = await account.File.SaveFileAsync(folder);
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> Download(int? id)
        //{
        //    Account account = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    string fileBytes = $"~/File/" + account.Attachment;

        //    return File(
        //        fileBytes,         /*string*/
        //        "application/pdf", /*mime type*/
        //        "fileName.pdf" /*name of the file (bax)*/
        //    );

        //}
        public async Task<IActionResult> Download(int? id)
        {
            Account account = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            string fileBytes = $"~/File/" + account.Attachment;
            string mimeType = "";
            string fileName = "";

            if (account.Attachment.EndsWith(".pdf"))
            {
                mimeType = "application/pdf";
                fileName = "fileName.pdf";
            }
            else if (account.Attachment.EndsWith(".docx"))
            {
                mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                fileName = "fileName.docx";
            }
            else if (account.Attachment.EndsWith(".xlsx"))
            {
                mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                fileName = "fileName.xlsx";
            }

            if (string.IsNullOrEmpty(fileBytes))
            {
                return NotFound();
            }
            return File(
                fileBytes,         /*string*/
               mimeType, /*mime type*/
                 fileName /*name of the file (bax)*/
            );

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Account dbAccount = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbAccount == null)
            {
                return BadRequest();
            }
            return View(dbAccount);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Account account)
        {

            if (id == null)
            {
                return NotFound();
            }
            Account dbAccount = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbAccount == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbAccount);
            }
            if (account.File != null)
            {
                if (!account.File.IsDocument())
                {
                    ModelState.AddModelError("File", "Zəhmıt olmasa sənəd seçin!");
                    return View(dbAccount);
                }
                string folder = Path.Combine(_env.WebRootPath, "File");
                string path = Path.Combine(folder, dbAccount.Attachment);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbAccount.Attachment = await account.File.SaveFileAsync(folder);
            }
            dbAccount.Title = account.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Account account = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (account == null)
            {
                return BadRequest();
            }
            if (account.IsDeactive)
            {
                account.IsDeactive = false;
            }
            else
            {
                account.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
