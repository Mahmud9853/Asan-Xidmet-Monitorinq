using Asan.DAL;
using Asan.Models;
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
    public class ContractController : Controller
    {
        private readonly AppDbContext _db;
      
        public ContractController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Contract> contracts =await _db.Contracts.OrderByDescending(x => x.Id).ToListAsync();
            return View(contracts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (contract.Title == null)
            {
                ModelState.AddModelError("Title", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            if (contract.Svg == null)
            {
                ModelState.AddModelError("Svg", "Zəhmət olmasa xananı doldurun !");
                return View();
            }
            await _db.Contracts.AddAsync(contract);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Contract dbContract = await _db.Contracts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbContract == null)
            {
                return BadRequest();
            }
            return View(dbContract);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Contract contract)
        {

            if (id == null)
            {
                return NotFound();
            }
            Contract dbContract = await _db.Contracts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbContract == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(dbContract);
            }
            dbContract.Svg = contract.Svg;
            dbContract.Title = contract.Title;
            dbContract.Text = contract.Text;
            dbContract.Description = contract.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
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
            return View(contract);
        }
        public async Task<IActionResult> Delete(int? id)
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
            return View(contract);

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
            Contract contract = await _db.Contracts.FirstOrDefaultAsync(x => x.Id == id);
            if (contract == null)
            {
                return BadRequest();

            }
            _db.Contracts.Remove(contract);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> GetData(int pageNumber, int pageSize)
        //{
        //    var data = await _db.Contracts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        //    return Ok(data);
        //}
    }
}
