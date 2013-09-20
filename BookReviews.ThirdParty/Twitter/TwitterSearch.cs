using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;


namespace BookReviews.ThirdParty.Twitter
{
    public class TwitterSearch : ITwitterSearch
    {
        private IOAuthCredentials _credentials = new InMemoryCredentials();
        private ApplicationOnlyAuthorizer _authorization;
        private TwitterContext _twitterContext;

        public TwitterSearch()
        {
        }

        public void Connect(string consumerKey, string consumerSecret)
        {
            if (_credentials.ConsumerKey == null || _credentials.ConsumerSecret == null)
            {
                _credentials.ConsumerKey = consumerKey;
                _credentials.ConsumerSecret = consumerSecret;
            }

            _authorization = new ApplicationOnlyAuthorizer
            {
                Credentials = _credentials
            };

            _authorization.Authorize();

            if (!_authorization.IsAuthorized)
            {
                return;
            }

            _twitterContext = new TwitterContext(_authorization);
        }

        public List<Status> GetCurrentTweets(List<string> screenNames)
        {
            List<Status> statusTweets = new List<Status>();

            foreach (string screenName in screenNames)
            {
                statusTweets.AddRange((from tweet in _twitterContext.Status
                                       where tweet.Type == StatusType.User
                                             && tweet.ScreenName == screenName
                                             && tweet.Count == 10
                                       select tweet)
                .ToList());
            }

            return statusTweets.OrderByDescending(x => x.CreatedAt).ToList();
        }
    }
}
