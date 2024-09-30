using BaketWishlist.DataAcsessLayer.Entity;
using BaketWishlist.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace BaketWishlist.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                ModelState.AddModelError("", "bu adda istifadeci movcuddur");
            }

            var createdUser = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(createdUser, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }

            var existUser = await _userManager.FindByNameAsync(model.Username);

            if (existUser == null)
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(existUser, model.Password, true, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return View();
            }
            if (model.ReturnUrl is not null)
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("index", "Home");

        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var exsistUser = await _userManager.FindByEmailAsync(model.Email);
            if (exsistUser == null)
            {
                ModelState.AddModelError("", "bele istifadeci movcud deyil");
                return View();
            }

            var resertToken = await _userManager.GeneratePasswordResetTokenAsync(exsistUser);

            var resertLink = Url.Action(nameof(ResertPassword), "Account", new {  model.Email, resertToken }, Request.Scheme, Request.Host.ToString());

            return View("EmailView",model:resertLink);
        }

        public IActionResult EmailView(string resetLink)
        {
            return View(model:resetLink);
        }

        public IActionResult ResertPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResertPassword(ReserPasswordViewModel model,string email,string resertToken)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            var exitUser = await _userManager.FindByEmailAsync(email);
            if (exitUser == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ResetPasswordAsync(exitUser,resertToken,model.Password);
            return RedirectToAction(nameof(Login));
        }


    }
}
