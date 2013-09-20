using System;
using System.Collections.Generic;
using BookReviews.Data.ViewModels;

namespace BookReviews.API.Controllers
{
    public interface IReviewsController
    {
        ReviewViewModel RandomReview();
        ReviewResultsViewModel SelectReviewsByDate(ReviewFilter filter);
    }
}
