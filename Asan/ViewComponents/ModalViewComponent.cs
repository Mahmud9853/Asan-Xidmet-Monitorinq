using Asan.DAL;
using Asan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewComponents
{
    public class ModalViewComponent :ViewComponent
    {
        private readonly AppDbContext _db;
        public ModalViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Contract contract = await _db.Contracts.FirstOrDefaultAsync();
            return View(contract);
        }
    }
}
