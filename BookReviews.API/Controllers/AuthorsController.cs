using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;
using BookReviews.Data.Services;
using BookReviews.ThirdParty.GoogleBooks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.API.Controllers
{
    public class AuthorsController : ApiController, IAuthorsController
    {
        IAuthorService _authors = new AuthorService(); 

        [HttpGet]
        public AuthorViewModel RandomAuthor()
        {
            return _authors.RandomAuthor();
        }

        // todo --> make the filter specific for authors (generalized base object)
        [HttpPost]
        public AuthorResultsViewModel SelectAuthorsByDate(AuthorFilter filter)
        {
            return _authors.SelectAuthorsByDate(filter);
        }

        [HttpGet]
        public AuthorViewModel AuthorById(string id)
        {
            return _authors.AuthorById(id);
        }
    }
}
