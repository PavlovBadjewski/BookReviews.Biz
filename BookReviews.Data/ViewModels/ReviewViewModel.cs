using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using BookReviews.Data.Models;

namespace BookReviews.Data.ViewModels
{
    public class ReviewViewModel
    {
        public String Id { get; set; }
        public int OriginalId { get; set; }
        public string BookTitle { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
        public PrintPublisher Publisher { get; set; }
        public int? AudioPublisher { get; set; }
        public DateTime? PublicationDate {get; set; }
        public string PublicationNotes { get; set; }
        public DateTime? ReadDate {get; set; }
        public bool? IsAudio { get; set; }
        public bool? Unabridged { get; set; }
        public int? NumberCassettes { get; set; }
        public List<string> Narrators {get; set; }
        public string NarrationRating {get; set; }
        public int? Genre {get; set; }
        public string Synopsis {get; set; }
        public int? PageCount {get; set; }
        public string ContentNotes {get; set; }
        public string ShortReview { get; set; }
        public string LongReview { get; set; }
        public int? YearProduced { get; set; }
        public GoogleBooksData Google { get; set; }
    }
}

