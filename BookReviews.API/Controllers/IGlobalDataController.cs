using System;
using System.Collections.Generic;
using BookReviews.Data.Models;

namespace BookReviews.API.Controllers
{
    public interface IGlobalDataController
    {
        List<Genre> Genres();
    }
}
