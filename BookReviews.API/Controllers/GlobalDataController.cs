using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using BookReviews.Data.Models;
using BookReviews.Data.Repositories;
using BookReviews.ThirdParty.GoogleBooks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.API.Controllers
{
    public class GlobalDataController : ApiController, IGlobalDataController
    {
        IGenreRepository _genres = new GenreRepository();

        [HttpGet]
        public List<Genre> Genres()
        {
            var genres = _genres.All();
            
            genres.Insert(0, new Genre()
            {
                Name = "--- Please Select a Genre ---",
                OriginalId = -1
            });

            return genres;
        }
    }
}