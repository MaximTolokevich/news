using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace news.Controllers.Models
{
    public class Author
    {
        
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string FullName { get; set; }
        
        public string Password { get; set; }
        
        public string Email { get; set; }
        public List<NewsViewModel> News { get; set; } = new List<NewsViewModel>();
    }

}
