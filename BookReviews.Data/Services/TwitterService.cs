using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using BookReviews.Data.ViewModels;

using BookReviews.ThirdParty.Twitter;
using LinqToTwitter;

using System.Web;


namespace BookReviews.Data.Services
{
    public class TwitterService : ITwitterService
    {
        private bool _persistTweets;
        private long _defaultSeconds;
        private Cache _cache;

        private const string CURRENT_TWEETS = "TWITTER_CURRENT_TWEETS";

        public TwitterService()
            : this(false, 0)
        {
        }

        public TwitterService(bool persistTweets, long defaultSeconds)
        {
            _persistTweets = persistTweets;
            _defaultSeconds = defaultSeconds;
            _cache = HttpContext.Current.Cache;
        }

        // ? return TwitterPayload with data and message?
        public List<TweetViewModel> GetCurrentTweets(double seconds, string consumerKey, string consumerSecret, List<string> screenNames)
        {
            object tweets = _cache[CURRENT_TWEETS];

            if (tweets == null)
            {
                // get new tweeks
                try
                {
                    var search = new TwitterSearch();

                    search.Connect(consumerKey, consumerSecret);
                    tweets = search.GetCurrentTweets(screenNames)
                        .Select(x => new TweetViewModel()
                        {
                            Text = FormatHtml(x.Text),
                            ScreenName = x.ScreenName,
                            CreatedAt = x.CreatedAt,
                            Source = x.Source,
                            Id = x.StatusID,
                            ProfileImage = x.User.ProfileImage,
                            ProfileImageUrl = x.User.ProfileImageUrl,
                            Place = x.Place,
                            ImageSize = x.User.ImageSize,
                            Location = x.User.Location,
                            Name = x.User.Name
                        })
                    .ToList();

                    // store tweets
                    _cache.Add(CURRENT_TWEETS, tweets, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch
                {
                    return null;
                }
            }

            if (tweets != null)
            {
                return (List<TweetViewModel>)tweets;
            }

            return null;
        }

        private string FormatHtml(string text)
        {
            int index = 0, newIndex = 0;
            var url = "";
            var formattedString = new StringBuilder("");

            while (index >= 0)
            {
                newIndex = text.IndexOf("http", index);

                if (newIndex > 0)
                {
                    formattedString.Append(text.Substring(index, newIndex - index));
                    index = newIndex;

                    newIndex = text.IndexOf(" ", index);

                    if (newIndex > 0)
                    {
                        url = text.Substring(index, newIndex - index);
                        formattedString.Append("<a href='" + url + "'>" + url + "</a>");
                        index = newIndex;
                    }
                    else
                    {
                        url = text.Substring(index);
                        formattedString.Append("<a href='" + url + "' target='_blank'>" + url + "</a>");
                        index = -1;
                    }
                }
                else
                {
                    formattedString.Append(text.Substring(index));
                    break; 
                }
            }

            return formattedString.ToString();
        }

        public List<TweetViewModel> GetCurrentTweets(string consumerKey, string consumerSecret, List<string> screenNames)
        {
            return GetCurrentTweets(_defaultSeconds, consumerKey, consumerSecret, screenNames);
        }
    }
}
