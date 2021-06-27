using AspAppMvcWithDb.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Pseudo")]
        [DataType(DataType.Text)]
        /*[ValidationUsername(allowDomain:"974", ErrorMessage = "username doit avoir 974")]*/
        [Remote(action: "isUserNameInUse", controller: "Account")]
        public string Username { get; set; }

        [Required]

        [Display(Name ="Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirmation de mot de passe")]
        [Compare("Password", ErrorMessage ="Votre mot de passe doit correspondre")]
        public string ConfirmPassword { get; set; }
    }
}
