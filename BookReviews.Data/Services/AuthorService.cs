using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using BookReviews.Data.Repositories;
using BookReviews.Data.ViewModels;

namespace BookReviews.Data.Services
{
    public class AuthorService : IAuthorService
    {
        private IBookReviewRepository _bookReviewRepository;
        private IAuthorRepository _authorRepository;

        public AuthorService()
            : this(new BookReviewRepository(), new AuthorRepository())
        {
        }

        public AuthorService(IBookReviewRepository bookReviewRepository, IAuthorRepository authorRepository)
        {
            _bookReviewRepository = bookReviewRepository;
            _authorRepository = authorRepository;
        }

        public AuthorResultsViewModel SelectAuthorsByDate(AuthorFilter filter)
        {
            var results = new AuthorResultsViewModel();
            filter = ValidateFilter(filter);

            var authors = _authorRepository
                .SelectAuthorsByDate(filter.PageIndex, filter.ItemsPerPage, filter)
                .Select(_authorRepository.BuildAuthorViewModel)
                .ToList();

            //foreach (var review in reviews)
            //{
            //    foreach (var author in review.Authors)
            //    {
            //        _authorRepository.AttachAuthorInformation(author);
            //    }
            //}

            results.Authors = authors;
            results.Filter = filter;

            return results;
        }

        public AuthorViewModel RandomAuthor()
        {
            return _authorRepository.BuildAuthorViewModel(_authorRepository.RandomAuthor());
        }

        //public List<AuthorViewModel> All()
        //{
        //    return _authorRepository.All()
        //        .Select(_authorRepository.BuildAuthorViewModel)
        //        .ToList();
        //}

        public AuthorViewModel AuthorById(string id)
        {
            return _authorRepository.BuildAuthorViewModel(_authorRepository.ById(id));
        }

        private AuthorFilter ValidateFilter(AuthorFilter filter)
        {
            filter.TotalItems = _authorRepository.GetTotalAuthors(filter);
            filter.TotalPages = filter.TotalItems / filter.ItemsPerPage + (filter.TotalItems % filter.ItemsPerPage > 0 ? 1 : 0);
            filter.PageIndex = filter.PageIndex >= 0
                ? ((filter.TotalPages - 1) >= filter.PageIndex ? filter.PageIndex : (filter.TotalPages - 1))
                : 0;

            return filter;
        }
    }
}
