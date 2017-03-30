using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreApp.Models;

namespace BookStoreApp.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    [HandleError]
    public class BookManagementController : Controller
    {
        private BookStoreDB db = new BookStoreDB();


        //Get Max BookId 
        public int GetBookIdCount()
        {
            int count = 0;
            var query = from book in db.Book
                        select book;
            count = query.Max(b => b.BookId);
            return count + 1;
        }

        // GET: /Admin/BookManagement/
        public ActionResult Index()
        {
            var book = db.Book.Include(b => b.Author).Include(b => b.Category).Include(b => b.Publisher);
            return View(book.ToList());
        }

        // GET: /Admin/BookManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: /Admin/BookManagement/Create
        public ActionResult Create(int? id)
        {
            if(id==null)
            {
                ViewBag.AuthorId = new SelectList(db.Author, "AuthorId", "AuthorName");
                ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
                ViewBag.PublisherId = new SelectList(db.Publisher, "PublisherId", "PublisherName");
                return View();
            }
            else
            {
                Book book = db.Book.Find(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AuthorId = new SelectList(db.Author, "AuthorId", "AuthorName", book.AuthorId);
                ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", book.CategoryId);
                ViewBag.PublisherId = new SelectList(db.Publisher, "PublisherId", "PublisherName", book.PublisherId);
                return View(book);
            }
            
        }

        // POST: /Admin/BookManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,CategoryId,Title,AuthorId,PublisherId,Description,Price,ISBN,PublicationDate,Image")] Book book)
        {
            if (ModelState.IsValid)
            {

                string imgUrl = HttpContext.Server.MapPath("~/Content/Image/BookId") + GetBookIdCount() + ".jpg";
                System.IO.File.Create(imgUrl, 1).Close();
                //string src = @"C:\Users\abhishekshi\Pictures\320x150.gif";
                System.IO.File.Copy(book.Image, imgUrl, true);
                book.Image = "/Content/Image/BookId" + GetBookIdCount() + ".jpg";
                
                

                if (book.BookId != 0)
                {
                    db.Entry(book).State = EntityState.Modified;
                    ViewBag.AuthorId = new SelectList(db.Author, "AuthorId", "AuthorName", book.AuthorId);
                    ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", book.CategoryId);
                    ViewBag.PublisherId = new SelectList(db.Publisher, "PublisherId", "PublisherName", book.PublisherId);

                }
                else
                {
                    ViewBag.AuthorId = new SelectList(db.Author, "AuthorId", "AuthorName", book.AuthorId);
                    ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", book.CategoryId);
                    ViewBag.PublisherId = new SelectList(db.Publisher, "PublisherId", "PublisherName", book.PublisherId);

                    db.Book.Add(book);
                }

            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: /Admin/BookManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Author, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", book.CategoryId);
            ViewBag.PublisherId = new SelectList(db.Publisher, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // POST: /Admin/BookManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BookId,CategoryId,Title,AuthorId,PublisherId,Description,Price,ISBN,PublicationDate,Image")] Book book)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(book).State = EntityState.Modified;
                
                //if(book.Image == )

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Author, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", book.CategoryId);
            ViewBag.PublisherId = new SelectList(db.Publisher, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // GET: /Admin/BookManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Admin/BookManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ValidateReview()
        {
            var review = from rev in db.BookReview
                         select rev;

            return View(review.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
