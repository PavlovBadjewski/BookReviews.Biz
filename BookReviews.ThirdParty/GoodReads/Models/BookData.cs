using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoodReads.Models
{
    public class BookData
    {
        public BookData()
        {
            Authors = new List<BookAuthorData>();
        }

        public long BookId { get; set; }
        public double AverageRating { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
        public string Isbn { get; set; }
        public string ImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string BookTitle { get; set; }
        public List<BookAuthorData> Authors { get; set; }
        public List<BookData> SimilarBooks { get; set; }
    }
}

