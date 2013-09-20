using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;

namespace BookReviews.Data.Repositories
{
    public interface IBookReviewRepository
    {
        List<Review> AllReviews { get; set; }
        Review ById(string id);
        List<Review> ByYear(int year);
        List<Review> ByAuthor(int author);
        List<Review> ByYearAndMonth(int year, int month);
        List<Review> ByGenre(int genre);
        Review RandomReview();
        List<Review> SelectReviewsByDate(int pageIndex, int reviewCount, int? genre);
        ReviewViewModel BuildReviewViewModel(Review r);
        int GetTotalReviews(ReviewFilter filter);
    }
}
