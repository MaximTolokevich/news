using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace news.Repositories.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public List<News> News { get; set; } = new List<News>();
    }

}
