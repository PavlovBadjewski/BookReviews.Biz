using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using BookReviews.Data.ViewModels;
using BookReviews.Data.Services;

namespace BookReviews.API.Controllers
{
    public class TwitterController : ApiController, ITwitterController
    {
        ITwitterService _twitter = new TwitterService(true, 20);

        [HttpGet]
        public List<TweetViewModel> CurrentTweets()
        {
            var screenNames = WebConfigurationManager.AppSettings["TwitterFeedScreenNames"]
                .Split(new char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            return _twitter.GetCurrentTweets(WebConfigurationManager.AppSettings["TwitterConsumerKey"], 
                WebConfigurationManager.AppSettings["TwitterConsumerSecret"], screenNames);
        }
    }
}
