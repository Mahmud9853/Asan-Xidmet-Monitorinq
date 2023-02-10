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
    public class ContractController : Controller
    {
        private readonly AppDbContext _db;

        public ContractController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            //ViewBag.Showscount = _db.Contracts.Count();
            List<Contract> contract = await _db.Contracts.ToListAsync();
            return View(contract);
        }
        [HttpGet]
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
            return PartialView( contract);
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
        //public async Task<IActionResult> ShowLoad(int skip)
        //{

        //    if (_db.Contracts.Count() <= skip)
        //    {
        //        return Content("Daxil olmaq qadağandır! ");
        //    }
        //    List<Contract> contracts = await _db.Contracts.Skip(skip).Take(6).ToListAsync();

        //    return PartialView("_ShowsLoad", contracts);

        //}
    }
}
