using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.ViewModels
{
    public class CreateRoleViewModel
    {
        [Display(Name ="Nom du rôle")]
        public string RoleName { get; set; }
    }
}
