using DapperCore.DAL;
using DapperCore.Model;
using DapperCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCRUD.Controllers
{
    public class BookController : Controller
    {
        private readonly DapperContext db;
        public BookController(DapperContext context)
        {
            this.db = context;
        }
        // GET: BookController
        public ActionResult Index()
        {
            List<BookViewModel> bookList = new List<BookViewModel>();
            var books = db.GetAllBooks();
            foreach (var book in books)
                bookList.Add(new BookViewModel()
                {
                    BookId = book.BookId,
                    BookAuthor = book.BookAuthor,
                    BookLanguageId = book.BookLanguageId,
                    BookLangugeName = db.GetLanguageById(book.BookLanguageId).LanguageName,
                    BookTitle = book.BookTitle

                });
            ;
            ViewBag.Languges = db.GetLanguage();
            return View(bookList);
        }


        // GET: BookController/Create
        public ActionResult Create()
        {
            ViewBag.Languges = db.GetLanguage();
            return View();
        }

        // POST: BookController/Create
        [HttpPost]

        public ActionResult Create(Book book)
        {
            try
            {
                db.SaveBook(book);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(Guid? id)
        {
            Book book = db.GetBookById(id);
            ViewBag.Languges = db.GetLanguage();
            return View("Create", book);
        }


        // GET: BookController/Delete/5
        public ActionResult Delete(Guid? id)
        {
            try
            {
                db.DeleteBook(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }



    }
}
