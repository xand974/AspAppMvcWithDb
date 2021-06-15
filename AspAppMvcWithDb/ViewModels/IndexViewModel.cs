using AspAppMvcWithDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> GetPosts { get; set; }
    }
}
