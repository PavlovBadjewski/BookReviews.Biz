using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoogleBooks.Models
{
    public class AccessDetail
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public bool embeddable { get; set; }
        public bool publicDomain { get; set; }
        public AccessMethod epub { get; set; }
        public AccessMethod pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
    }
}
