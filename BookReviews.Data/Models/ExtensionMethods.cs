using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Data.Models
{
    public static class MongoObjectExtensionMethods
    {
        public static Review Serializable(this Review value)
        {
            value._id = null;
            if (value.Google != null)
            {
                if (double.IsNaN(value.Google.AverageRating))
                {
                    value.Google.AverageRating = -1;
                }
            }
            return value;
        }

        public static Genre Serializable(this Genre value)
        {
            value._id = null;
            return value;
        }

        public static GoogleBooksData Serializable(this GoogleBooksData value)
        {
            if (value != null)
            {
                if (double.IsNaN(value.AverageRating))
                {
                    value.AverageRating = -1;
                }
            }

            return value;
        }

    }
}
