using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.ViewModels
{
    public class LoginViewModel 
    {
        [Required]
        [Display(Name = "Pseudo")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
