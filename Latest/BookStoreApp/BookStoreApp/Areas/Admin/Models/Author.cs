using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public Nullable<decimal> Phone { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}