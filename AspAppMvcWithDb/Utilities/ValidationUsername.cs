using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Utilities
{
    public class ValidationUsername : ValidationAttribute
    {
        private readonly string allowDomain;

        public ValidationUsername(string allowDomain)
        {
            this.allowDomain = allowDomain;
        }
        public override bool IsValid(object value)
        {
            //Split coupe une chaine de caractère en fonction d'un caractère et renvoie un tableau
            var usernames = value.ToString().Split("@");
            return usernames[1].ToUpper() == allowDomain.ToUpper();
        }
    }
}
