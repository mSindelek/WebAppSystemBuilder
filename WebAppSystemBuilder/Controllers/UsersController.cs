using WebAppSystemBuilder.Models;
using WebAppSystemBuilder.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAppSystemBuilder.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher, IPasswordValidator<AppUser> passwordValidator) : Controller
    {
        
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                AppUser userToAdd = new() { UserName = newUser.Name, Email = newUser.Email };
                IdentityResult result = await userManager.CreateAsync(userToAdd, newUser.Password);
                if (result.Succeeded) { return RedirectToAction("Index"); }
                else AddIdentityErrors(result);
            }
            return View(newUser);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditAsync(string id)
        {
            var userToEdit = await userManager.FindByIdAsync(id);
            if (userToEdit == null) { return View("NotFound"); }
            return View(userToEdit);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(string id, string email, string password)
        {
            AppUser? userToEdit = await userManager.FindByIdAsync(id);
            if (userToEdit == null) { return View("NotFound"); }
            if (!string.IsNullOrEmpty(email)) { userToEdit.Email = email; }
            else { ModelState.AddModelError("", "Email cannot be empty"); }
            IdentityResult? validPass = null;
            if (!string.IsNullOrEmpty(password)) 
            { 
                validPass = await passwordValidator.ValidateAsync(userManager,userToEdit,password);
                if (validPass.Succeeded)
                { userToEdit.PasswordHash = passwordHasher.HashPassword(userToEdit, password); }
                else AddIdentityErrors(validPass);
            }
            else { ModelState.AddModelError("", "Password cannot be empty"); }
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && validPass != null)
            {
                if (validPass.Succeeded)
                {
                    IdentityResult result = await userManager.UpdateAsync(userToEdit);
                    if (result.Succeeded)                    
                        return RedirectToAction("Index");                    
                    else AddIdentityErrors(result);
                }
            }
            return View(userToEdit);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var userToDelete = await userManager.FindByIdAsync(id);
            if (userToDelete != null) 
            { 
                IdentityResult result = await userManager.DeleteAsync(userToDelete);
                if (result.Succeeded) {  return RedirectToAction("Index"); }
                else { AddIdentityErrors(result); }
            }
            else { ModelState.AddModelError("", "User not found"); }
            return View("Index", userManager.Users);
        }
        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) { ModelState.AddModelError("", error.Description); }
        }
    }
}
