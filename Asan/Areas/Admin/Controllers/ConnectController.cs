using Asan.DAL;
using Asan.Helpers;
using Asan.Models;
using Asan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ConnectController : Controller
    {
        private readonly AppDbContext _db;

        public ConnectController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Connectcount = _db.Connects.Where(x => !x.IsReading).OrderByDescending(x=>x.Id).Count();
            List<Connect> connects = await _db.Connects.OrderByDescending(x=>x.Id).ToListAsync();
            return View(connects);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            
            if (id == null)
            {
                return NotFound();

            }
            Connect connects = await _db.Connects.FirstOrDefaultAsync(x => x.Id == id);
            if (connects == null)
            {
                return BadRequest();
            }
            return View(connects);
        }
        
        public async Task<IActionResult> Count(int? id)
        {
          
            if (id == null)
            {
                return NotFound();

            }
            Connect connects = await _db.Connects.FirstOrDefaultAsync(x => x.Id == id);
            if (connects == null)
            {
                return BadRequest();
            }
            if (connects.IsReading)
            {
                connects.IsReading = false; 
            }
            else
            {
                connects.IsReading = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index"); 
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Connect connects = await _db.Connects.FirstOrDefaultAsync(x => x.Id == id);
            if (connects == null)
            {
                return BadRequest();

            }
            return View(connects);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Connect connects = await _db.Connects.FirstOrDefaultAsync(x => x.Id == id);
            if (connects == null)
            {
                return BadRequest();

            }
            _db.Connects.Remove(connects);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult SendAll()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendAll(MailVM messageVM)
        {
            List<Connect> connects = await _db.Connects.ToListAsync();

            foreach (var connect in connects)
            {
                await Helper.SendMessage("Asan", messageVM.Message, connect.Email);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Send()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(MailVM messageVM, int? id , string email)
        {
            if (id == null)
            {
                return NotFound();
            }
            Connect connects = await _db.Connects.FirstOrDefaultAsync(x => x.Id == id && x.Email == email);
            if (connects == null)
            {
                return BadRequest();
            }
            await Helper.SendMessage("Asan", messageVM.Message, connects.Email);
            return RedirectToAction("Index");
        }
    }
}
