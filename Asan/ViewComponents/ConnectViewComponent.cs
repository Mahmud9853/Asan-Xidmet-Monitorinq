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
    public class ConnectViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public ConnectViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Connect connect = new Connect();


            //foreach (Connect item connects)
            //{

            //    if (connects.IsReading == true)
            //    {
            //        item.Count--;
            //    }
            //    else
            //    {
            //        item.Count++;
            //    }
            //}
            //await _db.SaveChangesAsync();
            return View(connect);
        }

   
    }
}
