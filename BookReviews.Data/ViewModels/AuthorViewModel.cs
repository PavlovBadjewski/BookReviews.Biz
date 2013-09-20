using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using BookReviews.Data.Models;

namespace BookReviews.Data.ViewModels
{
    public class AuthorViewModel
    {
        public String Id { get; set; }
        public int OriginalId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Notes { get; set; }
        public GoodReadsAuthor GoodReads { get; set; }
    }
}

