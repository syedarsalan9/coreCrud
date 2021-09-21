using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string bookName { get; set; }
        public string authorName { get; set; }
        public string bookEdition { get; set; }

    }
}
