using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace news.Controllers.Models
{
    public class Author
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public List<NewsViewModel> News { get; set; } = new List<NewsViewModel>();
    }

}
