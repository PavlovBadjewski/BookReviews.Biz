using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using BookReviews.Data.Repositories;
using BookReviews.Data.ViewModels;

namespace BookReviews.Data.Services
{
    public class BookReviewService : IBookReviewService
    {
        private IBookReviewRepository _bookReviewRepository;
        private IAuthorRepository _authorRepository;

        public BookReviewService()
            : this(new BookReviewRepository(), new AuthorRepository())
        {
        }

        public BookReviewService(IBookReviewRepository bookReviewRepository, IAuthorRepository authorRepository)
        {
            _bookReviewRepository = bookReviewRepository;
            _authorRepository = authorRepository;
        }

        public ReviewResultsViewModel SelectReviewsByDate(ReviewFilter filter)
        {
            var results = new ReviewResultsViewModel();
            filter = ValidateFilter(filter);

            var reviews = _bookReviewRepository
                .SelectReviewsByDate(filter.PageIndex, filter.ItemsPerPage, filter.Genre)
                .Select(_bookReviewRepository.BuildReviewViewModel)
                .ToList();

            foreach (var review in reviews)
            {
                foreach (var author in review.Authors)
                {
                    _authorRepository.AttachAuthorInformation(author);
                }
            }

            results.Reviews = reviews;
            results.Filter = filter;

            return results;
        }

        public ReviewViewModel ReviewById(string id)
        {
            var review = _bookReviewRepository.BuildReviewViewModel(_bookReviewRepository.ById(id));

            foreach (var author in review.Authors)
            {
                _authorRepository.AttachAuthorInformation(author);
            }

            return review;
        }

        // redundant
        private ReviewFilter ValidateFilter(ReviewFilter filter) {
            filter.TotalItems = _bookReviewRepository.GetTotalReviews(filter);
            filter.TotalPages = filter.TotalItems / filter.ItemsPerPage + (filter.TotalItems % filter.ItemsPerPage > 0 ? 1 : 0);
            filter.PageIndex = filter.PageIndex >= 0
                ? ((filter.TotalPages - 1) >= filter.PageIndex ? filter.PageIndex : (filter.TotalPages - 1))
                : 0;

            return filter;
        }

        public ReviewViewModel RandomReview()
        {
            var review = _bookReviewRepository.BuildReviewViewModel(_bookReviewRepository.RandomReview());

            foreach (var author in review.Authors)
            {
                _authorRepository.AttachAuthorInformation(author);
            }

            return review;
        }
    }
}
