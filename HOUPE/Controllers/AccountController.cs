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
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Linq;
using HOUPE;
using Microsoft.AspNetCore.Server.HttpSys;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Http.Authentication;
using AuthenticationManager = Microsoft.AspNetCore.Server.HttpSys.AuthenticationManager;
using System.Data;

namespace HOUPE.Controllers
{
  //  [Authorize(Roles ="admin,user")]
    public class AccountController : Controller
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        public readonly RoleManager<UserRole> _roleManager;
        public readonly UserContext _context;
        public string AdminEmail = "dorisdelis.13@gmail.com";


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager, UserContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
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
                        user.UserStatus = role1;
                        await _userManager.UpdateAsync(user);
                        await _userManager.AddToRoleAsync(user, role1);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "users");
                    }
                }
                else
                {
                    if (result.Succeeded)
                    {
                        user.UserStatus = role2;
                        await _userManager.UpdateAsync(user);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           UsersImage avatar;

            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var currentUser = await _userManager.FindByNameAsync(model.Email);
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        if (currentUser.UserStatus == "admin")
                        {
                            avatar = new UsersImage { ImageAdress = "admin" };

                            _context.Add(avatar);
                            _context.SaveChanges();
                            return RedirectToAction("Index", "Users");

                        }
                        else if (currentUser.UserStatus == "user")
                        {
                            avatar = new UsersImage{ ImageAdress = "user" };

                            _context.Add(avatar);
                            _context.SaveChanges();
                            return RedirectToAction("Index", "Home");

                        }
                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    avatar = new UsersImage { ImageAdress = "ups" };

                    _context.Add(avatar);
                    _context.SaveChanges();

                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
                return View(model);
        }
        

       
        


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        /* private AuthenticationManager AuthManager
         {
             get
             {
                 return HttpContext.GetOwinContext().Authentication;
             }
         }


         [Authorize]
         public ActionResult Logout()
         {

             var authManager = ServiceContainer.Resolve<AuthManager>();
             AuthManager.SignOut();
             return RedirectToAction("Index", "Home");
         }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
           
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}