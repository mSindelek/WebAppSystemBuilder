using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppSystemBuilder.Models;

namespace WebAppSystemBuilder.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) : Controller
    {
        readonly RoleManager<IdentityRole> _roleManager = roleManager;
        readonly UserManager<AppUser> _userManager = userManager;

        public IActionResult Index()
        {
            return View(_roleManager.Roles.OrderBy(role=>role.Name));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(string name) {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    AddIdentityErrors(result);
            }
            return View(name);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole? roleToDelete = await _roleManager.FindByIdAsync(id);
            if (roleToDelete != null)
            {
                var result = await _roleManager.DeleteAsync(roleToDelete);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    AddIdentityErrors(result);
            }
            ModelState.AddModelError("", "Role not found");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditAsync(string id)
        {
            IdentityRole? roleToEdit = await _roleManager.FindByIdAsync(id);
            if (roleToEdit != null)
            {
                List<AppUser> members = [];
                List<AppUser> nonMembers = [];
                foreach (var user in _userManager.Users)
                {
                    var list = await _userManager.IsInRoleAsync(user, roleToEdit.Name!) ? members : nonMembers;
                    list.Add(user);
                }
                return View(new RoleState { Members = members, NonMembers = nonMembers, Role = roleToEdit, });
            }
            ModelState.AddModelError("", "Role not found");
            return View(id);

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(RoleModification roleModification)
        {
            if (ModelState.IsValid)
            {
                foreach (string userId in roleModification.AddIds ?? [] )
                {
                    AppUser? userToAdd = await _userManager.FindByIdAsync(userId);
                    if (userToAdd != null)
                    {
                        IdentityResult result = await _userManager.AddToRoleAsync(userToAdd, roleModification.RoleName);
                        if (!result.Succeeded) { AddIdentityErrors(result); }
                    }
                }
                foreach (string userId in roleModification.DeleteIds ?? [])
                {
                    AppUser? userToAdd = await _userManager.FindByIdAsync(userId);
                    if (userToAdd != null)
                    {
                        IdentityResult result = await _userManager.RemoveFromRoleAsync(userToAdd, roleModification.RoleName);
                        if (!result.Succeeded) { AddIdentityErrors(result); }
                    }
                }
            }
            ModelState.AddModelError("", "Spatne zadana zmena, zkontroluj udaje");
            return RedirectToAction("Index");
        }

        private void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            { ModelState.AddModelError("", error.Description); }
        }
    }
}
