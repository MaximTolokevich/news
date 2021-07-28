using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace news.Controllers.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string newsContent { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateNews { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public int[] NewsAuthors { get; set; }
    }
}
