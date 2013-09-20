using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace BookReviews.Import
{
    public static class ExtensionMethods
    {
        public static int ToInt(this string value, int defaultValue)
        {
            int i = 0;

            if (!int.TryParse(value, out i))
            {
                i = defaultValue;
            }

            return i;
        }

        //bookData.BookId = book["id"].ToLong(); != null ? book["id"].InnerText : null;

        static List<string> months = new List<string>()
        {
            "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"
        };

        public static int ToMonthNumber(this string month)
        {
            month = month.ToLower();

            var monthNumber = months.IndexOf(month);

            return monthNumber >= 0 ? ++monthNumber : -1;
        }
    }
}
