using Asan.DAL;
using Asan.Models;
using Asan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db=db;
        }

        public async Task<IActionResult> Index()
        {
           

            //ViewBag.Slidercount.Image = _db.Sliders.Count();
            //List<Connect> connects = await _db.Connects.Where(x => x.IsDeactive).ToListAsync();
            List<Document> documents = await _db.Documents.Where(x =>x.IsDeactive).ToListAsync();
            List<Slider> sliders =await _db.Sliders.ToListAsync();
            List<Contract> contract = await _db.Contracts.Take(4).ToListAsync();
            List<AboutSlider> aboutSliders = await _db.AboutSliders.Where(x => x.IsDeactive).ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                AboutSliders= aboutSliders,
                Documents = documents,
                Contracts = contract,
                Sliders = sliders

            };
            return View(homeVM);
        }
        //public async Task<IActionResult> Download(int? id)
        //{
        //    Document document = await _db.Documents.FirstOrDefaultAsync(x => x.Id == id);
        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    string fileBytes = $"~/File/" + document.Attachment;

        //    return File(
        //        fileBytes,         /*string*/
        //        "application/pdf", /*mime type*/
        //        "fileName.pdf" /*name of the file (bax)*/
        //    );

        //}
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

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Contract contract = await _db.Contracts.FirstOrDefaultAsync(x => x.Id == id);
            if (contract == null)
            {
                return BadRequest();
            }
            return PartialView("_PartialDetailView", contract);
        }
        public async Task<IActionResult> DetailSlider(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return BadRequest();
            }
            return View(slider);
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
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
 
        //public async Task<IActionResult> Search(string Title)
        //{
        //    List<Document> documents = await _db.Documents.Where(x => x.IsDeactive).ToListAsync();
        //    List<Slider> sliders = await _db.Sliders.ToListAsync();
        //    List<Contract> contract = await _db.Contracts.ToListAsync();
        //    List<AboutSlider> aboutSliders = await _db.AboutSliders.Where(x => x.IsDeactive).ToListAsync();
        //    HomeVM homeVM = new HomeVM
        //    {
        //        AboutSliders = aboutSliders,
        //        Documents = documents,
        //        Contracts = contract,
        //        Sliders = sliders
        //    };
        //    return PartialView("_PartialSearch", homeVM);
        //}
    }
}
