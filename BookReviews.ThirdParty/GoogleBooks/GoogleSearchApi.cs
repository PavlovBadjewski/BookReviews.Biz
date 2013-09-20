using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.ThirdParty.GoogleBooks
{
    public class GoogleSearchApi : IGoogleSearchApi
    {
        private WebClient _webClient;

        public GoogleSearchApi()
        {
            _webClient = new WebClient();
            //client.Headers.Add("User-Agent", "Nobody"); //my endpoint needs this...
        }

        public BookCollection FindMatchingBooks(string authorName, string bookTitle)
        {
            var response = _webClient.DownloadString(new Uri(BuildSearchUri(authorName, bookTitle)));

            return JsonConvert.DeserializeObject<BookCollection>(response);
        }

        private string BuildSearchUri(string authorName, string bookTitle)
        {
            var query = "";
            var delimiter = "";

            if (!string.IsNullOrEmpty(bookTitle))
            {
                query += "intitle:" + bookTitle.ToLower().Replace(' ', '+');
                delimiter = "+";
            }

            if (!string.IsNullOrEmpty(authorName))
            {
                query += delimiter + "inauthor:" + authorName.ToLower().Replace(' ', '+');
                delimiter = "+";
            }

            return string.Format(Constants.GOOGLE_BOOKS_URL, query, ConfigurationManager.AppSettings["GoogleBooksKey"]);
        }
    }
}
