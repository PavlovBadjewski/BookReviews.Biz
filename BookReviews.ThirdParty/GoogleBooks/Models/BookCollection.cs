using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoogleBooks.Models
{
    public class BookCollection
    {
        public BookCollection()
        {
            items = new List<Book>();
        }

        public string kind { get; set; }
        public int totalItems { get; set; }
        public List<Book> items { get; set; }
    }
}
