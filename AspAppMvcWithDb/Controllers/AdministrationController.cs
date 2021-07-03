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

        public AdministrationController(RoleManager<IdentityRole> manager)
        {
            this.manager = manager;
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
            var model = new EditRoleViewModel()
            {
                roleId = roleFound.Id,
                RoleName = roleFound.Name
            };
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
        public IActionResult EditUsersInRole()
        {
            return View();
        }


    }

}
