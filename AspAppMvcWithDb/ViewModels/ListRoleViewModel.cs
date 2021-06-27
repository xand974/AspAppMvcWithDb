using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AspAppMvcWithDb.ViewModels
{
    public class ListRoleViewModel
    {
        public IEnumerable<IdentityRole> roles { get; set; }
    }
}
