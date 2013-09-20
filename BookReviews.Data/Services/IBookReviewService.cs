using System;
using System.Collections.Generic;
using System.Linq;

using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;

namespace BookReviews.Data.Services
{
    public interface IBookReviewService
    {
        ReviewResultsViewModel SelectReviewsByDate(ReviewFilter filter);
        ReviewViewModel RandomReview();
        ReviewViewModel ReviewById(string id);
    }
}
