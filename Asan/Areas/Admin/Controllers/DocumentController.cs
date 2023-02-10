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
    public class DocumentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public DocumentController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Document> documents = await _db.Documents.ToListAsync();
            return View(documents);
        }
        //public IActionResult Get(int pageNumber)
        //{
        //    int pageSize = 4;
        //    var documents =  _db.Documents.Skip((pageNumber + 1) * pageSize).Take(pageSize);
        //    return PartialView("_Data", documents);
        //}
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Document document)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (document.File == null)
            {
                ModelState.AddModelError("Document", "Error");
                return View();
            }

            if (!document.File.IsDocument())
            {
                ModelState.AddModelError("Document", "Error var");
                return View();
            }
            //if (!file.Photo.IsOlder1Mb())
            //{
            //    ModelState.AddModelError("Photo", "MAX 5Mb");
            //    return View();
            //}
            string folder = Path.Combine(_env.WebRootPath, "File");
            document.Attachment = await document.File.SaveFileAsync(folder);
            await _db.Documents.AddAsync(document);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Download(int? id)
        {
            Document document = await _db.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            string fileBytes = $"~/File/" + document.Attachment;
            string mimeType = "";
            string fileName = "";

            if (document.Attachment.EndsWith(".pdf"))
            {
                mimeType = "application/pdf";
                fileName = "fileName.pdf";
            }
            else if (document.Attachment.EndsWith(".docx"))
            {
                mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                fileName = "fileName.docx";
            }
            else if (document.Attachment.EndsWith(".xlsx"))
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
    


        //}
        //public async Task<IActionResult> Download(int? id)
        //{
        //    Document document = await _db.Documents.FirstOrDefaultAsync(x => x.Id == id);
        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    string fileBytes = $"~/File/" + document.Attachment;
        //    string mimeType = "";
        //    string fileName = "";

        //    if (document.Attachment.EndsWith(".pdf"))
        //    {
        //        mimeType = "application/pdf";
        //        fileName = "fileName.pdf";
        //    }
        //    else if (document.Attachment.EndsWith(".docx"))
        //    {
        //        mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //        fileName = "fileName.docx";
        //    }
        //    else if (document.Attachment.EndsWith(".xlsx"))
        //    {
        //        mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        fileName = "fileName.xlsx";
        //    }

        //    if (string.IsNullOrEmpty(fileBytes))
        //    {
        //        return NotFound();
        //    }

        //    return File(
        //        fileBytes,         /*string*/
        //        mimeType, /*mime type*/
        //        fileName /*name of the file*/
        //    );

        //}


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Document dbDocument = await _db.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDocument == null)
            {
                return BadRequest();
            }
            return View(dbDocument);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Document document)
        {

            if (id == null)
            {
                return NotFound();
            }
            Document dbDocument = await _db.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (dbDocument == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbDocument);
            }
            if (document.File != null)
            {
                if (!document.File.IsDocument())
                {
                    ModelState.AddModelError("File", "Zəhmıt olmasa sənəd seçin!");
                    return View(dbDocument);
                }
                string folder = Path.Combine(_env.WebRootPath, "File");
                string path = Path.Combine(folder, dbDocument.Attachment);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbDocument.Attachment = await document.File.SaveFileAsync(folder);
            }
            dbDocument.Title = document.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Document document = await _db.Documents.FirstOrDefaultAsync(x => x.Id == id);
            if (document == null)
            {
                return BadRequest();
            }
            if (document.IsDeactive)
            {
                document.IsDeactive = false;
            }
            else
            {
                document.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
