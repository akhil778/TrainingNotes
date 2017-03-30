using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreApp.Areas.Admin.Models;
using BookStoreApp.Models;

namespace BookStoreApp.Controllers
{
    [HandleError]
    public class BookReviewController : Controller
    {
        private BookStoreDB db = new BookStoreDB();

        // GET: /BookReview/
        public ActionResult Index()
        {
            return View(db.BookReview.ToList());
        }

        // GET: /BookReview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReview bookreview = db.BookReview.Find(id);
            if (bookreview == null)
            {
                return HttpNotFound();
            }
            return View(bookreview);
        }

        // GET: /BookReview/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BookReview/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,BookId,Description,FirstName,LastName")] BookReview bookreview)
        {
            if (ModelState.IsValid)
            {
                db.BookReview.Add(bookreview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookreview);
        }

        // GET: /BookReview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReview bookreview = db.BookReview.Find(id);
            if (bookreview == null)
            {
                return HttpNotFound();
            }
            return View(bookreview);
        }

        // POST: /BookReview/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,BookId,Description,FirstName,LastName")] BookReview bookreview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookreview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookreview);
        }

        // GET: /BookReview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookReview bookreview = db.BookReview.Find(id);
            if (bookreview == null)
            {
                return HttpNotFound();
            }
            return View(bookreview);
        }

        // POST: /BookReview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookReview bookreview = db.BookReview.Find(id);
            db.BookReview.Remove(bookreview);
            db.SaveChanges();
            return RedirectToAction("Index");
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
