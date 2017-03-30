using BookStoreApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class BookStoreDB : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<BookReview> BookReview { get; set; }

        public System.Data.Entity.DbSet<BookStoreApp.Models.ViewModel.ShoppingCartViewModel> ShoppingCartViewModels { get; set; }
    }
}