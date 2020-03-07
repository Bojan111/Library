using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Core.Auth;
using LibraryMVCProjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVCProjects.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var role = roleManager.Roles;
            return View(role);
        }
        public IActionResult AddNewRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewRole(AddRoleViewModel addRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addRoleViewModel);
            }
            var role = new IdentityRole
            {
                Name = addRoleViewModel.RoleName
            };
            IdentityResult result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(addRoleViewModel);
        }
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var editroleviewmodel = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
            };

            foreach (var item in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(item, role.Name))
                {
                    editroleviewmodel.Users.Add(item.UserName);
                }
            }
            return View(editroleviewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editRoleViewModel);
            }
            var role = await roleManager.FindByIdAsync(editRoleViewModel.Id);

            if (role != null)
            {
                role.Name = editRoleViewModel.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Role not updated, something went wrong!");
                return View(editRoleViewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong while deleting this role!");
                }
            }
            else
            {
                ModelState.AddModelError("", "This role can't be found");
            }
            return View("Index", roleManager.Roles);
        }
        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToPage("Index");
            }
            var adduserstoroleviewmodel = new UserRoleViewModel { RoleId = role.Id };
            foreach (var item in userManager.Users)
            {
                 if (!await userManager.IsInRoleAsync(item, role.Name))
                 {
                    adduserstoroleviewmodel.Users.Add(item);
                 }
            }
            return View(adduserstoroleviewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                var roletemp = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);
                foreach (var item in userManager.Users)
                {
                    if(!await userManager.IsInRoleAsync(item, roletemp.Name))
                    {
                        userRoleViewModel.Users.Add(item);
                    }
                }
                return View(userRoleViewModel);
            }
            var user = await userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(userRoleViewModel);
        }
        public async Task<IActionResult> DeleteUserFromRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if(role == null)
            {
                return RedirectToAction("Index");
            }
            var addUserToRoleViewModel = new UserRoleViewModel { RoleId = role.Id };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserToRoleViewModel.Users.Add(user);
                }
            }

            return View(addUserToRoleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(UserRoleViewModel userRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                var roletemp = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);
                foreach (var users in userManager.Users)
                {
                    if(!await userManager.IsInRoleAsync(users, roletemp.Name))
                    {
                        userRoleViewModel.Users.Add(users);
                    }
                }
                return View(userRoleViewModel);
            }
            var user = await userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            var result = await userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(userRoleViewModel);
        }
    }
}