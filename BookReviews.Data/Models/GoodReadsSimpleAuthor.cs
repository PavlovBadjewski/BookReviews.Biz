using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.Data.Models
{
    public class GoodReadsSimpleAuthor
    {
        public long AuthorId { get; set; }
        public string Name { get; set; }
        public int? InternalAuthorId { get; set; }
    }
}
