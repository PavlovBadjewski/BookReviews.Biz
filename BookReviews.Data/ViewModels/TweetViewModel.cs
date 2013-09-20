using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using BookReviews.Data.Models;
using LinqToTwitter;

namespace BookReviews.Data.ViewModels
{
    public class TweetViewModel
    {
        public string Text { get; set; }
        public string ScreenName { get; set; }
        public string Source { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ProfileUrl { get; set; }
        public Place Place { get; set; }
        public ProfileImageSize ImageSize { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }
}

