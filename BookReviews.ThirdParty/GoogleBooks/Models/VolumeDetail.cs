using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.ThirdParty.GoogleBooks.Models
{
    public class VolumeDetail
    {
        public VolumeDetail()
        {
            industryIdentifiers = new List<IndustryIdentifier>();
            authors = new List<string>();
            categories = new List<string>();
            imageLinks = new Dictionary<string, string>();
        }

        public string title { get; set; }
        public string subtitle { get; set; }
        public List<string> authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public List<IndustryIdentifier> industryIdentifiers { get; set; }
        public int pageCount { get; set; }
        public string printType { get; set; }
        public List<string> categories { get; set; }
        public float averageRating { get; set; }
        public int ratingsCount { get; set; }
        public string contentVersion { get; set; }
        public Dictionary<string, string> imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
    }
}