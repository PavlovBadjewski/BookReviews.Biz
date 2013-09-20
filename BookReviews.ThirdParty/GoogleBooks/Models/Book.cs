using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoogleBooks.Models
{
    public class Book
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public VolumeDetail volumeInfo { get; set; }
        public SaleDetail saleInfo { get; set; }
        public AccessDetail accessInfo { get; set; }
        public SearchDetail searchInfo { get; set; }
    }
}
