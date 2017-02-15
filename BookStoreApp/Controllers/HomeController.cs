using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    public class HomeController : Controller
    {
        private BooksDBEntities _db = new BooksDBEntities(); 
        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Book bookToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                _db.Books.Add(bookToCreate);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var bookToEdit = (from b in _db.Books
                              where b.Id == id
                              select b).First();

            return View(bookToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Book bookToEdit)
        {
            try
            {
                var originalBook = (from b in _db.Books
                                    where b.Id == bookToEdit.Id
                                    select b).First();
                if (!ModelState.IsValid)
                    return View(originalBook);
                _db.ApplyPropertyChanges(originalBook, bookToEdit);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var bookToDelete = (from b in _db.Books
                              where b.Id == id
                              select b).First();

            return View(bookToDelete);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(Book bookToDelete)
        {
            try
            {
                var originalBook = (from b in _db.Books
                                    where b.Id == bookToDelete.Id
                                    select b).First();
                if (!ModelState.IsValid)
                    return View(originalBook);
                _db.ApplyPropertyChanges(originalBook);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
