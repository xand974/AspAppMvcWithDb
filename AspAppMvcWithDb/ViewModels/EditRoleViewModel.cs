using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.ViewModels
{
    public class EditRoleViewModel : CreateRoleViewModel
    {
        [Display(Name ="Numéro du rôle")]
        public string roleId { get; set; }
    }
}
