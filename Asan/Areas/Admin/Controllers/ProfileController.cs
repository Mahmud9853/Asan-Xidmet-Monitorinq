using Asan.DAL;
using Asan.Models;
using Asan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class ProfileController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        public ProfileController(IWebHostEnvironment env, AppDbContext db, UserManager<Appuser> userManager, SignInManager<Appuser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _env = env;
            _db = db;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            else
            {
                Appuser appuser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (appuser == null)
                {
                    return BadRequest();
                }
                ProfileVM profileVM = new ProfileVM
                {
                    Id = appuser.Id,
                    Email = appuser.Email,
                    FullName = appuser.FullName,
                    UserName = appuser.UserName,
                    Image = appuser.Image,
                    Role = (await _userManager.GetRolesAsync(appuser)).FirstOrDefault()
                };
                return View(profileVM);
            }
        }
        public async Task<IActionResult> AdminProfile()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            Appuser appuser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (appuser == null)
            {
                return BadRequest();
            }
            ProfileVM profileVM = new ProfileVM
            {
                Id = appuser.Id,
                Email = appuser.Email,
                FullName = appuser.FullName,
                UserName = appuser.UserName,
                Image = appuser.Image,
            };



            return PartialView("_PartialAdminProfile", "profileVM");
        }
    }
}
