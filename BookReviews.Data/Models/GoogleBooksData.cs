using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.Data.Models
{
    public class GoogleBooksData
    {
        public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
        public Dictionary<string, string> ImageLinks { get; set; } // smallThumbnail, thumbnail
        public List<string> Categories { get; set; }
        public double AverageRating { get; set; }
        public int RatingsCount { get; set; }
    }
}
