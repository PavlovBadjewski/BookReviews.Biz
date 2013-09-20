using System;
using System.Collections.Generic;
//using System.Data.Entity;

using System.Configuration;
using System.Data.Odbc;

using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BookReviews.Data;
using BookReviews.Data.Models;

namespace BookReviews.Import
{
    public class Console
    {
        static void Main(string[] args)
        {
            var create = false;
            var import = false;
            var google = false;
            var goodreads = false;
            var migrate = false;

            new ImportDatabase(createDatabase: create, importData: import, addGoogleData: google, addGoodReads: goodreads, migrate: migrate);
        }
    }
}



