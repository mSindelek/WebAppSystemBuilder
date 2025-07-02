using WebAppSystemBuilder.Models;
using WebAppSystemBuilder.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAppSystemBuilder.Controllers
{
    [Authorize]
    public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
    {
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new()
            {
                UserName = "",
                Password = "",
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                AppUser? userToLogin = await userManager.FindByNameAsync(login.UserName);
                if (userToLogin != null)
                {
                    /*Microsoft.AspNetCore.Identity.SignInResult*/
                    var signInResult = await signInManager.PasswordSignInAsync(userToLogin,login.Password,login.Remember,false);

                    if (signInResult.Succeeded)
                        return Redirect(login.ReturnUrl ?? "/");
                }
            }
            ModelState.AddModelError("", "User not found or wrong password");
            return View(login);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsGuest(LoginViewModel login) {
            AppUser? userToLogin = await userManager.FindByNameAsync("Guest");
            if (userToLogin != null) {
                await signInManager.SignInAsync(userToLogin, login.Remember);
                return Redirect(login.ReturnUrl ?? "/");
            }
            ModelState.AddModelError("", "Failed to Login");
            return View(login);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsEditor(LoginViewModel login) {
            AppUser? userToLogin = await userManager.FindByNameAsync("Editor");
            if (userToLogin != null) {
                await signInManager.SignInAsync(userToLogin, login.Remember);
                return Redirect(login.ReturnUrl ?? "/");
            }
            ModelState.AddModelError("", "Failed to Login");
            return View(login);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
