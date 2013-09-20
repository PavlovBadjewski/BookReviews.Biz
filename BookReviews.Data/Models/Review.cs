using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace BookReviews.Data.Models
{
    public class Review : MongoObject
    {
        public int OriginalId { get; set; }                     // [Key]
        public string BookTitle { get; set; }           // [Book Title]
        public List<int> Authors { get; set; }          // [Author], [Co-Author]
        public PrintPublisher Publisher { get; set; }   // [Audio Publisher], [Publisher], [Publisher Division], [City and State]
        public int? AudioPublisher { get; set; }         // [Audio Publisher], [Publisher], [Publisher Division], [City and State]
        public DateTime? PublicationDate {get; set; }    // [Pub Year], [Pub Month]
        public string PublicationNotes { get; set; }    // [Publication Notes]
        public DateTime? ReadDate {get; set; }           // [Yr Read], [Mo Read], [MonthNum]
        public bool? IsAudio {get; set; }                // [Audio Book]
        public bool? Unabridged {get; set; }             // [Unabridged]
        public int? NumberCassettes {get; set; }         // [Number of Cassettes]
        public List<string> Narrators {get; set; }      // [Narrator1], [Narrator2], [Narrator3]
        public string NarrationRating {get; set; }      // [How]
        public int? Genre {get; set; }                // [Genre]
        public string Synopsis {get; set; }             // [Synopsis]
        public int? PageCount {get; set; }               // [Number of Pages]
        public string ContentNotes {get; set; }         // [Content Notes]
        public string ShortReview {get; set; }          // [Brief Review]
        public string LongReview { get; set; }           // [Review]
        public int? YearProduced { get; set; }            // [Year Produced]
        public GoogleBooksData Google { get; set; }
        public GoodReadsBook GoodReads { get; set; }
        public DateTime? GoodReadsAccessDate { get; set; }
        public DateTime? GoogleBooksAccessDate { get; set; }
    }
}

