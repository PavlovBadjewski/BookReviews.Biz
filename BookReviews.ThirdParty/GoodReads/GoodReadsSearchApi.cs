using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using BookReviews.ThirdParty.GoodReads;
using BookReviews.ThirdParty.GoodReads.Models;

namespace BookReviews.ThirdParty.GoodReads
{
    public class GoodReadsSearchApi : IGoodReadsSearchApi
    {
        private WebClient _webClient;
        private static string _apiKey = ConfigurationManager.AppSettings["GoodReadsApiKey"];
        private static string _apiSecret = ConfigurationManager.AppSettings["GoodReadsApiSecret"];

        public GoodReadsSearchApi()
        {
            _webClient = new WebClient();
            //client.Headers.Add("User-Agent", "Nobody"); //my endpoint needs this...
        }

        public BookData FindGoodReadsDataByIsbn(string isbn)
        {
            BookData bookData = null;

            try
            {
                var response = _webClient.DownloadString(new Uri(string.Format(Constants.API_URL + Constants.SEARCH_BY_ISBN, isbn, _apiKey)));

                var doc = new XmlDocument();
                doc.LoadXml(response);

                var root = doc.DocumentElement;

                bookData = GetBookData(root.SelectSingleNode("/GoodreadsResponse/book"));
                bookData.SimilarBooks = new List<BookData>();

                foreach (XmlNode book in root.SelectSingleNode("/GoodreadsResponse/book/similar_books"))
                {
                    var similar = GetBookData(book);
                    similar.Url = FindGoodReadsUrlByIsbn(similar.Isbn);
                    bookData.SimilarBooks.Add(similar);
                }
            }
            catch
            {
            }

            return bookData;
        }

        public BookAuthorData FindGoodReadsDataByAuthorId(string authorId)
        {
            var response = _webClient.DownloadString(new Uri(string.Format(Constants.API_URL + Constants.SEARCH_BY_AUTHOR_ID, authorId, _apiKey)));

            var doc = new XmlDocument();
            doc.LoadXml(response);

            var root = doc.DocumentElement;

            var authorData = GetAuthorData((XmlElement)root.SelectSingleNode("/GoodreadsResponse/author"));
            //authorData.OtherWorks = new List<BookData>();

            foreach (XmlNode book in root.SelectSingleNode("/GoodreadsResponse/author/books"))
            {
                var other = GetBookData(book);
                //other.Url = FindGoodReadsUrlByIsbn(other.Isbn);
                authorData.Works.Add(other);
            }

            return authorData;
        }

        public int FindGoodReadsAuthorIdByName(string lastName, string firstName)
        {
            int retVal = 0;

            var response = _webClient.DownloadString(new Uri(string.Format(Constants.API_URL + Constants.SEARCH_BY_AUTHOR_NAME, firstName + " " + lastName, _apiKey)));

            var doc = new XmlDocument();
            doc.LoadXml(response);

            var authors = doc.GetElementsByTagName("author");

            if (authors.Count == 1)
            {
                if (!int.TryParse(authors[0].Attributes["id"].Value, out retVal))
                {
                    retVal = -1; // BAD VALUE IN ID FIELD
                }
            }
            else
            {
                if (authors.Count == 0)
                {
                    retVal = -10; // NOT FOUND
                }
                else
                {
                    retVal = -11; // MULTIPLE FOUND;
                }
            }

            return retVal;
            //return JsonConvert.DeserializeObject<BookCollection>(response);
        }

        //public int FindGoodReadsBookIdByTitle(string title)
        //{
        //    int retVal = 0;

        //    var response = _webClient.DownloadString(new Uri(string.Format(Constants.API_URL + Constants.SEARCH_BY_BOOK_TITLE, title, Constants.API_KEY)));

        //    var doc = new XmlDocument();
        //    doc.LoadXml(response);

        //    var authors = doc.GetElementsByTagName("author");

        //    if (authors.Count == 1)
        //    {
        //        if (!int.TryParse(authors[0].Attributes["id"].Value, out retVal))
        //        {
        //            retVal = -1; // BAD VALUE IN ID FIELD
        //        }
        //    }
        //    else
        //    {
        //        if (authors.Count == 0)
        //        {
        //            retVal = -10; // NOT FOUND
        //        }
        //        else
        //        {
        //            retVal = -11; // MULTIPLE FOUND;
        //        }
        //    }

        //    return retVal;
        //    //return JsonConvert.DeserializeObject<BookCollection>(response);
        //}

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

            return string.Format(Constants.API_URL, query, _apiKey);
        }

        private string FindGoodReadsUrlByIsbn(string isbn)
        {
            var response = _webClient.DownloadString(new Uri(string.Format(Constants.API_URL + Constants.SEARCH_BY_ISBN, isbn, _apiKey)));

            var doc = new XmlDocument();
            doc.LoadXml(response);

            var root = doc.DocumentElement;

            var book = root.SelectSingleNode("/GoodreadsResponse/book");

            return book["url"] != null ? book["url"].InnerText : (book["link"] != null ? book["link"].InnerText : null);
        }

        private BookData GetBookData(XmlNode book)
        {
            BookData bookData = new BookData();

            bookData.BookId = book["id"].InnerTextToLong(-1);
            bookData.AverageRating = book["average_rating"].InnerTextToDouble(-1);
            bookData.Url = book["url"].InnerTextToString("");
            bookData.Link = book["link"].InnerTextToString("");
            bookData.ImageUrl = book["image_url"].InnerTextToString("");
            bookData.SmallImageUrl = book["small_image_url"].InnerTextToString("");
            bookData.Isbn = book["isbn"].InnerTextToString("");
            bookData.BookTitle = book["title"].InnerTextToString("");

            //bookData.Authors = ((List<XmlElement>)book["authors"].GetElementsByTagName("author").GetEnumerator()).Select(GetAuthorData);


            foreach (XmlElement author in book["authors"].GetElementsByTagName("author"))
            {
                bookData.Authors.Add(GetAuthorData(author));
            }

            return bookData;
        }

        private BookAuthorData GetAuthorData(XmlElement author)
        {
            return new BookAuthorData()
            {
                AuthorId = author["id"].InnerTextToLong(-1),
                ImageUrl = author["image_url"].InnerTextToString(""),
                SmallImageUrl = author["small_image_url"].InnerTextToString(""),
                Link = author["link"].InnerTextToString(""),
                Name = author["name"].InnerTextToString(""),
                AverageRating = author["average_rating"].InnerTextToDouble(-1),
                RatingsCount = author["ratings_count"].InnerTextToInt(-1),
                Gender = author["gender"].InnerTextToString(""),
                Hometown = author["hometown"].InnerTextToString(""),
                WorksCount = author["works_count"].InnerTextToInt(0),
                DateOfBirth = author["born_at"].InnerTextToDate(null),
                DateOfDeath = author["died_at"].InnerTextToDate(null)
                //books
            };
        }
    }
}
