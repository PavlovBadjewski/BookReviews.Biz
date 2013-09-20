using System;
using System.Collections.Generic;
using BookReviews.Data.ViewModels;

namespace BookReviews.API.Controllers
{
    public interface ITwitterController
    {
        List<TweetViewModel> CurrentTweets();
    }
}
