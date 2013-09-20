using System;
using System.Collections.Generic;
using System.Linq;

using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;

namespace BookReviews.Data.Services
{
    public interface ITwitterService
    {
        List<TweetViewModel> GetCurrentTweets(string consumerKey, string consumerSecret, List<string> screenNames);
        List<TweetViewModel> GetCurrentTweets(double seconds, string consumerKey, string consumerSecret, List<string> screenNames);
    }
}
