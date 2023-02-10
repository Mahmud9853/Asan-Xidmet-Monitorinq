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
    public class LegislationController : Controller
    {
        private readonly AppDbContext _db;

        public LegislationController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<LegislationCategory> legislationCategories = await _db.LegislationCategory.Include(x => x.Legislations).Where(x => x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
            List<Legislation> legislations = await _db.Legislations.Include(x => x.LegislationCategory).Where(x => x.IsDeactive).ToListAsync();
            AboutVM aboutVM = new AboutVM
            {
                Legislations = legislations,
                LegislationCategories = legislationCategories
            };
            return View(aboutVM);
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
        //public async Task<IActionResult> Download(int? id)
        //{
        //    Legislation legislation = await _db.Legislations.Include(x => x.LegislationCategory).Where(x => x.IsDeactive).FirstOrDefaultAsync(x => x.Id == id);
        //    if (legislation == null)
        //    {
        //        return NotFound();
        //    }

        //    string fileBytes = $"~/File/" + legislation.Attachment;

        //    return File(
        //        fileBytes,         /*string*/
        //        "application/pdf", /*mime type*/
        //        "fileName.pdf" /*name of the file (bax)*/
        //    );

        //}
        public async Task<IActionResult> Download(int? id)
        {
            Legislation legislation = await _db.Legislations.Include(x => x.LegislationCategory).Where(x => x.IsDeactive).FirstOrDefaultAsync(x => x.Id == id);
            if (legislation == null)
            {
                return NotFound();
            }

            string fileBytes = $"~/File/" + legislation.Attachment;
            string mimeType = "";
            string fileName = "";

            if (legislation.Attachment.EndsWith(".pdf"))
            {
                mimeType = "application/pdf";
                fileName = "fileName.pdf";
            }
            else if (legislation.Attachment.EndsWith(".docx"))
            {
                mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                fileName = "fileName.docx";
            }
            else if (legislation.Attachment.EndsWith(".xlsx"))
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

    }
}
