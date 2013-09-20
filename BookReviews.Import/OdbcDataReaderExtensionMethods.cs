using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Import
{
    public static class OdbcDataReaderExtensionMethods
    {
        public static string StringValue(this OdbcDataReader reader, int ordinal, string defaultValue)
        {
            var value = reader.GetValue(ordinal);

            return value.GetType() != typeof(System.DBNull) ? reader.GetString(ordinal) : defaultValue;
        }

        public static string StringValue(this OdbcDataReader reader, int ordinal)
        {
            return StringValue(reader, ordinal, null);
        }

        public static int? Int16Value(this OdbcDataReader reader, int ordinal, int? defaultValue)
        {
            var value = reader.GetValue(ordinal);

            return value.GetType() != typeof(System.DBNull) ? reader.GetInt16(ordinal) : defaultValue;
        }

        public static int? Int16Value(this OdbcDataReader reader, int ordinal)
        {
            return Int16Value(reader, ordinal, null);
        }

        public static int? Int32Value(this OdbcDataReader reader, int ordinal, int? defaultValue)
        {
            var value = reader.GetValue(ordinal);

            return value.GetType() != typeof(System.DBNull) ? reader.GetInt32(ordinal) : defaultValue;
        }

        public static int? Int32Value(this OdbcDataReader reader, int ordinal)
        {
            return Int32Value(reader, ordinal, null);
        }

        public static bool? BoolValue(this OdbcDataReader reader, int ordinal, bool? defaultValue)
        {
            var value = reader.GetValue(ordinal);

            return value.GetType() != typeof(System.DBNull) ? reader.GetBoolean(ordinal) : defaultValue;
        }

        public static bool? BoolValue(this OdbcDataReader reader, int ordinal)
        {
            return BoolValue(reader, ordinal, null);
        }

        public static DateTime? ParseDate(this DateTime? date, int year, int month, int day)
        {
            try
            {
                date = new DateTime(year, month, day);
            }
            catch
            {
                date = null;
            }
            return date;
        }
    }
}
