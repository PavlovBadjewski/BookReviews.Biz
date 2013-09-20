using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoodReads.Models
{
    public class BookAuthorData
    {
        public BookAuthorData()
        {
            Works = new List<BookData>();
        }

        public long AuthorId { get; set; }
        public string ImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public double AverageRating { get; set; }
        public int RatingsCount { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string Hometown { get; set; }
        public int WorksCount { get; set; }
        public List<BookData> Works { get; set; }
    }
}
