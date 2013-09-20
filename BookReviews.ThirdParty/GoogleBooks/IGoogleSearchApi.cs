using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using BookReviews.ThirdParty.GoogleBooks.Models;

namespace BookReviews.ThirdParty.GoogleBooks
{
    public interface IGoogleSearchApi
    {
        BookCollection FindMatchingBooks(string authorName, string bookTitle);
    }
}
