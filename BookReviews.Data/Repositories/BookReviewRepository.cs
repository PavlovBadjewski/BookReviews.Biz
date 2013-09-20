using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace BookReviews.Data.Repositories
{
    public class BookReviewRepository : RepositoryBase, IBookReviewRepository
    {
        public List<Review> AllReviews { get; set; }

        public BookReviewRepository() : base()
        {
            if (Database != null)
            {
                AllReviews = Database.GetCollection<BsonDocument>("Review").AsQueryable<Review>().ToList();
            }
        }

        public int GetTotalReviews(ReviewFilter filter)
        {
            if (filter.Genre.HasValue)
            {
                return AllReviews
                    .Where(x => x.Genre == filter.Genre)
                    .Count();
            }
            else
            {
                return (int)AllReviews.Count();
            }
        }

        public List<Review> ByYear(int year)
        {
            var startDate = new BsonDateTime(new DateTime(year, 1, 1));
            var endDate = new BsonDateTime(new DateTime(year + 1, 1, 1));

            var vals1 = AllReviews
                .Where(x => x.ReadDate >= new DateTime(year, 1, 1))
                .Where(x => x.ReadDate < new DateTime(year + 1, 1, 1))
                .OrderBy(x => x.ReadDate)
                .Take(10)
                .ToList();

            return vals1;
        }

        public List<Review> ByYearAndMonth(int year, int month)
        {
            throw new NotImplementedException();
        }

        public Review ById(string id)
        {
            return AllReviews.SingleOrDefault(x => x._id.ToString() == id);
        }

        public List<Review> ByAuthor(int author)
        {
            return AllReviews
                .Where(x => x.Authors.Contains(author))
                .ToList();
        }

        public List<Review> ByGenre(int genre)
        {
            return AllReviews
                .Where(x => x.Genre.HasValue && x.Genre.Value == genre)
                .ToList();
        }

        public Review RandomReview()
        {
            var position = (int)(DateTime.Now.Ticks % AllReviews.Count());

            return AllReviews
                //.Where(x => x.Google != null)
                .Skip(position)
                .FirstOrDefault();
        }

        public List<Review> SelectReviewsByDate(int pageIndex, int reviewCount, int? genre)
        {
            if (genre.HasValue)
            {
                return AllReviews
                    .Where(x => x.Genre == genre)
                    .OrderBy(x => x.ReadDate)
                    .Skip(pageIndex * reviewCount)
                    .Take(reviewCount)
                    .ToList();
            }
            else
            {
                return AllReviews
                    .OrderBy(x => x.ReadDate)
                    .Skip(pageIndex * reviewCount)
                    .Take(reviewCount)
                    .ToList();
            }
        }

        public ReviewViewModel BuildReviewViewModel(Review r)
        {
            return new ReviewViewModel()
            {
                AudioPublisher = r.AudioPublisher,
                Authors = r.Authors.Select(x => new AuthorViewModel() {
                    OriginalId = x
                }).ToList(),
                BookTitle = r.BookTitle,
                ContentNotes = r.ContentNotes,
                Genre = r.Genre,
                Google = r.Google.Serializable(),
                Id = r._id.ToString(),
                IsAudio = r.IsAudio,
                LongReview = r.LongReview,
                NarrationRating = r.NarrationRating,
                Narrators = r.Narrators,
                NumberCassettes = r.NumberCassettes,
                OriginalId = r.OriginalId,
                PageCount = r.PageCount,
                PublicationDate = r.PublicationDate,
                PublicationNotes = r.PublicationNotes,
                Publisher = r.Publisher,
                ReadDate = r.ReadDate,
                ShortReview = r.ShortReview,
                Synopsis = r.Synopsis,
                Unabridged = r.Unabridged,
                YearProduced = r.YearProduced
            };
        }

        private static void CreateAuthorViewModelStub()
        {
           // return new AuthorViewModel();
        }
    }
}




//public List<Review> ByYearAndMonth(int year, int month)
//{
//    var thisMonth = new DateTime(year, month, 1);
//    var nextMonth = new DateTime(month == 12 ? (year + 1) : year, month == 12 ? 1 : (month + 1), 1);

//    return Reviews.FindAs<Review>(
//        Query.And(
//            Query.GTE("ReadDate", new BsonDateTime(thisMonth)),
//            Query.LT("ReadDate", new BsonDateTime(nextMonth))
//        )
//    )
//    .OrderBy(x => x.ReadDate)
//    .ToList();
//}