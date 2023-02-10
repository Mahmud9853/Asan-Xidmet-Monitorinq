using Asan.DAL;
using Asan.Helpers;
using Asan.Models;
using Asan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<Appuser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Appuser> _signInManager;
        public UsersController(IWebHostEnvironment env, AppDbContext db,UserManager<Appuser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Appuser> signInManager)
        {
      
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Appuser> users = await _userManager.Users.OrderByDescending(x => x.Id).ToListAsync();
            List<UserVM> userVMs = new List<UserVM>();
            foreach (Appuser user in users)
            {
                UserVM userVM = new UserVM
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = user.Id,
                    Image=user.Image,
                    IsDeactive = user.IsDeactive,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                };
                userVMs.Add(userVM);
            }
            return View(userVMs);
        }
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Appuser appUser = new Appuser
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.UserName,

            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();

            }

            await _userManager.AddToRoleAsync(appUser, Helper.Roles.Admin.ToString());

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Appuser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            UpdateVM dbUpdateVM = new UpdateVM
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Image = user.Image,
            };
            return View(dbUpdateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, UpdateVM updateVM, Appuser appUser)
        {
            if (id == null)
            {
                return NotFound();
            }
            Appuser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            UpdateVM dbUpdateVM = new UpdateVM
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Image = updateVM.Image,

            };
            if (!ModelState.IsValid)
            {
                return View(dbUpdateVM);
            }
            bool isExist = await _db.Users.AnyAsync(x => x.UserName == updateVM.UserName && x.Id != appUser.Id);
            if (isExist)
            {
                ModelState.AddModelError("", "Username is alrready exist");
                return View(dbUpdateVM);
            }

            if (appUser.Photo != null)
            {
                if (!appUser.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please choose the image flie");
                    return View(user);
                }

                string folder = Path.Combine(_env.WebRootPath, "img");
                user.Image = await appUser.Photo.SaveFileAsync(folder);
            }
            user.FullName = updateVM.FullName;
            user.UserName = updateVM.UserName;
            user.Email = updateVM.Email;
            bool selfuser = false;
            if (User.Identity.Name == dbUpdateVM.UserName)
            {
                selfuser = true;
            }
            await _userManager.UpdateAsync(user);

            if (selfuser)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, true);

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Appuser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return BadRequest();
            }
            if (appUser.IsDeactive)
            {
                appUser.IsDeactive = false;
            }
            else
            {
                appUser.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return NotFound();

            }
            Appuser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return BadRequest();
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id, ResetPasswordVM resetPassword)
        {
            if (id == null)
            {
                return NotFound();

            }
            Appuser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return BadRequest();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            IdentityResult identityResult = await _userManager.ResetPasswordAsync(appUser, token, resetPassword.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction("Index");
        }

    }
}
