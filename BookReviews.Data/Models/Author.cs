using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace BookReviews.Data.Models
{
    public class Author : MongoObject
    {
        public int OriginalId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Notes { get; set; }
        public GoodReadsAuthor GoodReads { get; set; }
        public DateTime? GoodReadsAccessDate { get; set; }
    }
}
