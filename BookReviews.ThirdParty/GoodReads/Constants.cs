using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoodReads
{
    public class Constants
    {
        public const string API_URL = "http://www.goodreads.com/";
        public const string SEARCH_BY_AUTHOR_NAME = "api/author_url/{0}?key={1}";
        public const string SEARCH_BY_AUTHOR_ID = "author/show.xml?id={0}&key={1}";
        public const string SEARCH_BY_ISBN = "book/isbn?format=xml&isbn={0}&key={1}";
        public const string SEARCH_BY_GOODREADS_ID = "book/show?format=xml&key={0}&id={1}";
    }
}
