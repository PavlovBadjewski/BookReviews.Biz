using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Data.ViewModels
{
    public class AuthorFilter
    {
        public int ItemsPerPage { get; set; }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int? Genre { get; set; }
    }
}
