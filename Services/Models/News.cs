using System;
using System.Collections.Generic;

namespace news.Services.Models
{
    public class News
    {
        public int Id { get; set; }      
        public string newsContent { get; set; }
        public DateTime DateNews { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }     
        public IEnumerable<Author> NewsAuthors { get; set; } 

    }
}
