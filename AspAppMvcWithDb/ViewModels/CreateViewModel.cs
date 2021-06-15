using AspAppMvcWithDb.Models;
using System.ComponentModel.DataAnnotations;

namespace AspAppMvcWithDb.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Minimum 1 charactère s'il vous plaît !")]
        [MaxLength(40, ErrorMessage = "Maximum 40 charactère s'il vous plaît !")]
        public string Title { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "Minimum 1 charactère s'il vous plaît !")]
        public string Description { get; set; }


        public Creator Creator { get; set; }
    }
}
