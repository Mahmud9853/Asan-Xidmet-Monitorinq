using Asan.DAL;
using Asan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public FooterViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM footervm = new FooterVM
            {
                Informations = await _db.Informations.Where(x=>x.IsDeactive).ToListAsync(),
                Informationuses = await _db.Informationuses.Where(x => x.IsDeactive).ToListAsync()

            };
            return View(footervm);
        }
    }
}
