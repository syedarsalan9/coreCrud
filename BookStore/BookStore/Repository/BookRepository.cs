using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> getBook()
        {
            return DataSource().ToList();
        }
        public BookModel getBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookModel> searchBook(string name,string authorName,string edition)
        {
            return DataSource().Where(x => x.bookName.Contains(name) || x.authorName.Contains(authorName) && x.bookEdition.Contains(edition) ).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>(){ 
                new BookModel(){ Id = 1,bookName = "Maths",authorName="Syed Arsalan Ahmed",bookEdition= "7th Edition"},
                new BookModel(){ Id = 2,bookName = "Maths",authorName="Syed Arsalan Ahmed",bookEdition= "8th Edition"},
                new BookModel(){ Id = 3,bookName = "Medical Science & Management",authorName="Syed Ahsan Sulaiman",bookEdition= "6th Edition"},
                new BookModel(){ Id = 4,bookName = "Automobile Industry",authorName="Hamza Ghouri",bookEdition= "7th Edition"},
                new BookModel(){ Id = 5,bookName = "Business Management",authorName="Tariq Chippa",bookEdition= "9th Edition"},
                new BookModel(){ Id = 6,bookName = "Drugs",authorName="Syed Shamir Ali",bookEdition= "10th Edition"}
            };
        }
    }
}
