using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using BookReviews.Data.Models;

namespace BookReviews.Data.ViewModels
{
    public class ReviewResultsViewModel
    {
        public List<ReviewViewModel> Reviews { get; set; }
        public ReviewFilter Filter { get; set; }
    }
}

