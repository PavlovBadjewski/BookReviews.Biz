using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.Data.Models
{
    public class GoodReadsSimpleBook
    {
        public long BookId { get; set; }
        public string BookTitle { get; set; }
        public int? InternalBookId { get; set; }
    }
}
