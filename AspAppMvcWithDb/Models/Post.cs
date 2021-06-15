using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1 , ErrorMessage = "Minimum 1 charactère s'il vous plaît !")]
        [MaxLength(40 , ErrorMessage = "Maximum 40 charactère s'il vous plaît !")]
        public string Title { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "Minimum 1 charactère s'il vous plaît !")]
        public string Description { get; set; }


        public Creator Creator { get; set; }
    }
}
