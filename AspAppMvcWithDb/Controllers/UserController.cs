using AspAppMvcWithDb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> manager;
        private readonly ILogger<UserController> logger;

        public UserController(UserManager<IdentityUser> manager ,
                              SignInManager<IdentityUser> signInManager,
                              ILogger<UserController> logger )
        {
            this.manager = manager;
            SignInManager = signInManager;
            this.logger = logger;
        }

        public SignInManager<IdentityUser> SignInManager { get; }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new()
                {
                    UserName = model.Username
                };

                //Creation user dans la DB + hash password
                var result = await manager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //si on réussit à créer un compte, alors on l'authentifie directement en envoyant un cookie 
                    await SignInManager.SignInAsync(user, isPersistent: false) ;
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description, error.Code);

                    //va retourner les erreurs directement dans la View
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(userName : model.Username,
                                                                     password : model.Password, 
                                                                     isPersistent : model.RememberMe , 
                                                                     lockoutOnFailure : false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {

                        return RedirectToAction(controllerName: "Home", actionName: "Index");

                    }
                }

                ModelState.AddModelError("", "Invalid login");
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(controllerName:"Home",actionName:"Index");
        }

    }
}
