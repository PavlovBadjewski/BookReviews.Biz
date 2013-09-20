using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoogleBooks.Models
{
    public class SaleDetail
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }
        public Price listPrice { get; set; }
        public Price retailPrice { get; set; }
        public string buyLink { get; set; }
    }
}
