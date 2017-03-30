using BookStoreApp.Areas.Admin.Models;
using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    [HandleError(View="Error")]
    public class HomeController : Controller
    {
        BookStoreDB db = new BookStoreDB();

        public ActionResult Index()
        {
            //var query = from bookReview in db.BookReview
            //            select bookReview;

            var query = (from bookReview in db.BookReview
                         where bookReview.isValid == true
                         select bookReview).OrderByDescending(m => m.id).Take(10); 


            IEnumerable<BookReview> bookList = query.AsEnumerable<BookReview>();
            return View(bookList);
        }


        [Authorize]
        public ActionResult CreateBookReview(int? id)
        {

            var book = db.Book.Find(id);
            ViewBag.UserName = User.Identity.Name.ToString();
            return View(book);

        }
        //POST
        [Authorize]
        public ActionResult BookReview(int bookId, string description, string name)
        {
            
                BookReview br = new BookReview();
                br.BookId = bookId;
                
                br.Description = description;
                br.UserName = name;                          
                db.BookReview.Add(br);
                db.SaveChanges();

                return RedirectToAction("Index");
        }

        public ActionResult Search(string authorList, string publisherList, string bookTitle)
        {
            var aList = new List<string>();
            var pList = new List<string>();

            var authorQry = from d in db.Book
                           orderby d.Author.AuthorName
                           select d.Author.AuthorName;

            aList.AddRange(authorQry.Distinct());
            ViewBag.authorList = new SelectList(aList);

            var publisherQry = from d in db.Book
                               orderby d.Publisher.PublisherName
                               select d.Publisher.PublisherName;

            pList.AddRange(publisherQry.Distinct());
            ViewBag.publisherList = new SelectList(pList);

            var book = from m in db.Book
                         select m;

            if (!String.IsNullOrEmpty(authorList))
            {
                book = book.Where(s => s.Author.AuthorName.Contains(authorList));
            }
            if (!String.IsNullOrEmpty(publisherList))
            {
                book = book.Where(s => s.Publisher.PublisherName.Contains(publisherList));
            }
            if (!string.IsNullOrEmpty(bookTitle))
            {
                book = book.Where(x => x.Title.Contains(bookTitle));
            }

            return View(book);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Browse(string category)
        {
            // Retrieve Categories and its Associated Books from database
            //var genreModel = storeDB.Genres.Include("Albums")
            //    .Single(g => g.Name == genre);
            var categoryModel = db.Category.Include("Books")
                .Single(c => c.CategoryName == category);
            return View(categoryModel);

        }

        public ActionResult Details(int id)
        {
            var book = db.Book.Find(id);
            
           
            //var album = storeDB.Albums.Find(id);
            //return View(album);
            return View(book);

        }

        // GET: /CategoryMenu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = db.Category.ToList();
            
            return PartialView(categories);
        }

       

        // POST: /BookReview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,BookId,Description,FirstName,LastName")] BookReview bookreview)
        {
            if (ModelState.IsValid)
            {
                db.BookReview.Add(bookreview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookreview);
        }
    }
}