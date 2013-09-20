using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.Data.Models
{
    public class GoodReadsAuthor
    {
        public GoodReadsAuthor()
        {
            Works = new List<GoodReadsSimpleBook>();
        }

        public long AuthorId { get; set; }
        public double AverageRating { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string Gender { get; set; }
        public string Hometown { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public int RatingsCount { get; set; }
        public string SmallImageUrl { get; set; }
        public List<GoodReadsSimpleBook> Works { get; set; }
        public int WorksCount { get; set; }
    }
}
