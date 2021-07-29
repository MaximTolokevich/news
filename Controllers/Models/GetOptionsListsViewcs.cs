using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace news.Controllers.Models
{
    public class GetOptionsListsViewcs
    {
        public int Id { get; set; }
        [Required]
        public string newsContent { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateNews { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable< SelectListItem >CategoryList { get; set; }
        public IEnumerable<Author> NewsAuthors { get; set; }
        public int[] SelectedAuthors { get; set; }
    }
}
