using AspAppMvcWithDb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> manager;
        private readonly UserManager<IdentityUser> identityRole;

        public AdministrationController(RoleManager<IdentityRole> manager, UserManager<IdentityUser> identityRole)
        {
            this.manager = manager;
            this.identityRole = identityRole;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identity = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var result = await manager.CreateAsync(identity);
                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: "GetListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        [Authorize]
        public IActionResult GetListRoles()
        {
            var roles = manager.Roles;
            var model = new ListRoleViewModel()
            {
                roles = roles
            };
            return View("Roles",model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var roleFound =  await manager.FindByIdAsync(id);
            
            if(roleFound == null)
            {
                return View("HomeError");
            }
            
            var model = new EditRoleViewModel()
            {
                roleId = roleFound.Id,
                RoleName = roleFound.Name,
                
            };

            foreach (var user in identityRole.Users)
            {
                if (User.IsInRole(user.UserName))
                {
                    model.User.Add(user.UserName);
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await manager.FindByIdAsync(model.roleId);

                role.Name = model.RoleName;

                var result =await manager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(controllerName: "Administration", actionName: "GetListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId; 
            var roleFound = await manager.FindByIdAsync(roleId);
            if (roleFound == null)
                return View("HomeError");

            var models = new List<UserRoleViewModel>();
            foreach (var user in identityRole.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    
                };
                if(await identityRole.IsInRoleAsync(user,  roleFound.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                models.Add(userRoleViewModel);
            }

            return View(models);
        }


    }

}
