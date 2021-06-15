using AspAppMvcWithDb.Models;

namespace AspAppMvcWithDb.ViewModels
{
    public class CreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Creator Creator { get; set; }
    }
}
