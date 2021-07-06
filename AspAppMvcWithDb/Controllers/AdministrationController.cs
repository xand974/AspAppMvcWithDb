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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> manager, UserManager<IdentityUser> identityRole)
        {
            this.roleManager = manager;
            this.userManager = identityRole;
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
                var result = await roleManager.CreateAsync(identity);
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
            var roles = roleManager.Roles;
            var model = new ListRoleViewModel()
            {
                roles = roles
            };
            return View("Roles",model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var roleFound =  await roleManager.FindByIdAsync(id);
            
            if(roleFound == null)
            {
                return View("HomeError");
            }
            
            var model = new EditRoleViewModel()
            {
                roleId = roleFound.Id,
                RoleName = roleFound.Name,
                
            };

            foreach (var user in userManager.Users)
            {
                if (User.IsInRole(roleFound.Name))
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
                var role = await roleManager.FindByIdAsync(model.roleId);

                role.Name = model.RoleName;

                var result =await roleManager.UpdateAsync(role);
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
            var roleFound = await roleManager.FindByIdAsync(roleId);
            if (roleFound == null)
                return View("HomeError");

            var models = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                //Creer une instance du ViewModel pour chaque user de la list
                //on va rajouter l'id et name du user dans le role correspondant
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    
                };

                //Populate isSelected
                //Check si un user est déjà membre du role
                if(User.IsInRole(roleFound.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                //ajout du user dans la list
                models.Add(userRoleViewModel);
            }

            //passer à la view pour faire le rendu
            return View(models);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">on utilise le meme model passé dans la view + HTTPGET </param>
        /// <param name="roleId">vient du param dans la route</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model , string roleId)
        {
            //recup le role depuis la db
            var roleFound = await roleManager.FindByIdAsync(roleId);
            if (roleFound == null) return View("HomeError");

            //loop a traver le model (list des userrole)
            for (int i = 0; i < model.Count; i++)
            {
                //recup le user ou on a coché
                var userFound = await userManager.FindByIdAsync(model[i].UserId);

                //initialiser IdentityResult
                IdentityResult result = null;

                var isUserInrole = await userManager.IsInRoleAsync(user: userFound, roleFound.Name);
                //AJOUT DU ROLE AU USER
                //verif pour quel user on a coché isSelected
                //verif si le user n'est pas encore un membre du role
                if (model[i].IsSelected && !isUserInrole)
                {
                    //ajout du user avec le role correspondant
                   result = await userManager.AddToRoleAsync(userFound, roleFound.Name);
                }


                //SUPPRESSION ROLE AU USER
                //si le user est déselectionné + est membre d'un role, alors on le supprime
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user: userFound, roleFound.Name)))
                {
                    //ajout du user avec le role correspondant
                    result = await userManager.RemoveFromRoleAsync(userFound, roleFound.Name);
                }
                //pour les autres cas
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    return RedirectToAction(controllerName: "Administration", actionName: "GetListRoles");
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
