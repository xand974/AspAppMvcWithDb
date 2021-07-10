using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.ViewModels
{
    public class EditUserViewModel
    {
        [Display(Name ="Id")]
        public string UserId { get; set; }

        [Display(Name ="Nom d'utilisateur")]
        public string UserName { get; set; }
    }
}
