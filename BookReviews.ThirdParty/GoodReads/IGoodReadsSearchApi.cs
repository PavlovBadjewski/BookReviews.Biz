using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using BookReviews.ThirdParty.GoodReads.Models;

namespace BookReviews.ThirdParty.GoodReads
{
    public interface IGoodReadsSearchApi
    {
        int FindGoodReadsAuthorIdByName(string lastName, string firstName);
        BookData FindGoodReadsDataByIsbn(string isbn);
        BookAuthorData FindGoodReadsDataByAuthorId(string authorId);
    }
}
