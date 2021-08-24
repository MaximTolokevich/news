
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace news.Controllers.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        
        public IEnumerable<GetOptionsListsViewcs> News { get; set; }
    }
}
