using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HOUPE.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using HOUPE.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace PosterStore.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        public readonly RoleManager<UserRole> _roleManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Password = model.Password };
                string role1 = "admin";
                string role2 = "user";
                string AdminEmail = "dorisdelis.13@gmail.com";
               

                if (await _roleManager.FindByNameAsync(role1) == null)
                {
                    await _roleManager.CreateAsync(new UserRole(role1));
                }
                if (await _roleManager.FindByNameAsync(role2) == null)
                {
                    await _roleManager.CreateAsync(new UserRole(role2));
                }
                var result = await _userManager.CreateAsync(user, model.Password);




                if (user.Email == AdminEmail)
                {
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role1);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role2);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "home");
                    }
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        
    }
}