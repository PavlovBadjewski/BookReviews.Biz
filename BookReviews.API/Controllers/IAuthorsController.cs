using System;
using System.Collections.Generic;
using BookReviews.Data.ViewModels;

namespace BookReviews.API.Controllers
{
    public interface IAuthorsController
    {
        AuthorViewModel RandomAuthor();
        AuthorResultsViewModel SelectAuthorsByDate(AuthorFilter filter);
    }
}
