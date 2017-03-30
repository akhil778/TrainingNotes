using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStoreApp.Models;

namespace BookStoreApp.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public int id { get; set; }
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}