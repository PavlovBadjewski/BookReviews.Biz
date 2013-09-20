using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using BookReviews.Data.Models;

namespace BookReviews.Data.ViewModels
{
    public class AuthorResultsViewModel
    {
        public List<AuthorViewModel> Authors { get; set; }
        public AuthorFilter Filter { get; set; }
    }
}

