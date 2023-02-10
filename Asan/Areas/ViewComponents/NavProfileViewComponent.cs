using Asan.DAL;
using Asan.Models;
using Asan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Areas.ViewComponents
{
    [Area("Admin")]
    public class NavProfileViewComponent :ViewComponent
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public NavProfileViewComponent(AppDbContext db, UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Appuser users = await _userManager.FindByNameAsync(User.Identity.Name);
            UserVM userVM = new UserVM
            {
                UserName = users.UserName,
                FullName = users.FullName,
                Image = users.Image,
                Role = (await _userManager.GetRolesAsync(users)).FirstOrDefault()

            };
            return View(userVM);


        }
    }
}
