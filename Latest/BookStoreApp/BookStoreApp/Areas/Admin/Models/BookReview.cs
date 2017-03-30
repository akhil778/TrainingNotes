using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreApp.Areas.Admin.Models
{
    public class BookReview
    {
        [Key]
        public int id { get; set; }
        
        public int BookId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public bool isValid { get; set; }

        public virtual Book Book { get; set; }
    }
}