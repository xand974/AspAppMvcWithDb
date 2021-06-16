using AspAppMvcWithDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.ViewModels
{
    public class EditViewModel : CreateViewModel
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
    }
}
