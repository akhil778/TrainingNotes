using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class Cart
    {

        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime DateCreated { get; set; }
        public virtual Book Book { get; set; }
    }
}