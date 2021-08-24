using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace news.Controllers.Models
{
    public class GetOptionsListsViewcs
    {
        public int Id { get; set; }     
        public string newsContent { get; set; }
        public DateTime DateNews { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable< SelectListItem >CategoryList { get; set; }
        public IEnumerable<Author> NewsAuthors { get; set; }
        public int[] SelectedAuthors { get; set; }
    }
}
