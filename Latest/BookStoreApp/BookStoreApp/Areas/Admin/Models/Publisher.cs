using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class Publisher
    {
        public Publisher()
        {
            this.Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}