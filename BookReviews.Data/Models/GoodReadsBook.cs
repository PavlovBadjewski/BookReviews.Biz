using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.Data.Models
{
    public class GoodReadsBook
    {
        public long GoodReadsBookId { get; set; }
        public int? InternalBookId { get; set; }
        public double AverageRating { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string BookTitle { get; set; }
        //public string BookAuthors { get; set; }
        public List<GoodReadsSimpleAuthor> Authors { get; set; }
        public List<GoodReadsBook> SimilarBooks { get; set; }
    }
}
