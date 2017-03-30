using BookStoreApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        //[Required(ErrorMessage = "Category is required")]
        //[DisplayName("Category Name")]       
        public int CategoryId { get; set; }

        //[Required(ErrorMessage = "Book Title is required")]
        //[DisplayName("Book Title")] 
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }

        //[StringLength(160)]
        public string Description { get; set; }
        
        public int Price { get; set; }
        public string ISBN { get; set; }
        
        public Nullable<System.DateTime> PublicationDate { get; set; }
        public string Image { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual List<BookReview> BookReview { get; set; }
    }
}