using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository bookRespository;
        public BookController()
        {
            bookRespository = new BookRepository();
        }
        public IActionResult Index()
        {
            return View();
        }

        public List<BookModel> GetAllBooks()
        {
            return bookRespository.getBook();
        }

        public BookModel getSpecificBook(int id)
        {

            return bookRespository.getBookById(id);
        }

        public List<BookModel> searchBook(string name, string authorName, string bookEdition)
        {
            return bookRespository.searchBook(name, authorName, bookEdition);
        }
    }
}
