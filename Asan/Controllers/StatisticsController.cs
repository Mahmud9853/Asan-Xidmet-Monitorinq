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
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _db;

        public StatisticsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
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
