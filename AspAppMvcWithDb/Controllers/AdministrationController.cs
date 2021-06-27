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

        public IActionResult GetListRoles()
        {
            var roles = manager.Roles;
            var model = new ListRoleViewModel()
            {
                roles = roles
            };
            return View("Roles",model);
        }
    }

}
