using System;
using System.Collections.Generic;
using System.Linq;

using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;

namespace BookReviews.Data.Services
{
    public interface IAuthorService
    {
        AuthorResultsViewModel SelectAuthorsByDate(AuthorFilter filter);
        AuthorViewModel RandomAuthor();
        AuthorViewModel AuthorById(string id);
    }
}
