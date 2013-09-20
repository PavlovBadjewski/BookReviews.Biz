using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json.Serialization;

using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;
using BookReviews.Data.Services;
using BookReviews.ThirdParty.GoogleBooks;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.API.Controllers
{
    public class ReviewsController : ApiController, IReviewsController
    {
        IBookReviewService _reviews = new BookReviewService();

        [HttpGet]
        public ReviewViewModel RandomReview()
        {
            return _reviews.RandomReview();
        }

        [HttpGet]
        public ReviewViewModel ReviewById(string id)
        {
            return _reviews.ReviewById(id);
        }

        [HttpPost]
        public ReviewResultsViewModel SelectReviewsByDate(ReviewFilter filter)
        {
            return _reviews.SelectReviewsByDate(filter);
        }
    }
}

//year by author, month, pages
//all by author
//all by pages
//audio pub
//authors
//genre

//// GET api/values
//public IEnumerable<Review> Get()
//{
//    //return new string[] { "value1", "value2" };
//    return new List<Review>();
//}

//// GET api/values/5
//public string Get(string key)
//{
//    return "value";
//}

//// POST api/values
//public void Post(Review value)
//{
//}

//// PUT api/values/5
//public void Put(Review value)
//{
//}

//// DELETE api/values/5
//public void Delete(string key)
//{
//}