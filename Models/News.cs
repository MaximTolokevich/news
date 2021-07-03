using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace news.Models
{
    public class News
    {
        public int Id { get; set; }
        [Required]
        public string newsContent { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateNews { get; set; }
        [Required]
        public Category Category { get; set; }
        public List<Author> NewsAuthors { get; set; } = new List<Author>();

    }
}
