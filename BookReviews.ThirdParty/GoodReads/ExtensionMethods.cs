using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BookReviews.ThirdParty.GoodReads
{
    public static class GoodReadsExtensionMethods
    {

        public static long InnerTextToLong(this XmlNode value, long defaultValue)
        {
            long l = defaultValue;

            if (value != null)
            {
                if (!long.TryParse(value.InnerText, out l))
                {
                    l = defaultValue;
                }
            }

            return l;
        }

        public static double InnerTextToDouble(this XmlNode value, double defaultValue)
        {
            double d = defaultValue;

            if (value != null)
            {
                if (!double.TryParse(value.InnerText, out d))
                {
                    d = defaultValue;
                }
            }

            return d;
        }

        public static int InnerTextToInt(this XmlNode value, int defaultValue)
        {
            int i = defaultValue;

            if (value != null)
            {
                if (!int.TryParse(value.InnerText, out i))
                {
                    i = defaultValue;
                }
            }

            return i;
        }

        public static string InnerTextToString(this XmlNode value, string defaultValue)
        {
            string s = defaultValue;

            if (value != null)
            {
                s = value.InnerText;
            }

            return s;
        }

        public static DateTime? InnerTextToDate(this XmlNode value, DateTime? defaultValue)
        {
            DateTime d = defaultValue.HasValue ? defaultValue.Value : DateTime.MinValue;

            if (value != null)
            {
                var date = value.InnerText;

                if (!string.IsNullOrEmpty(date))
                {
                    DateTime.TryParse(value.InnerText, out d);
                }
            }

            if (d == DateTime.MinValue)
            {
                return null;
            }

            return (DateTime?)d;
        }

    }
}
