using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using BookReviews.Data;
using BookReviews.Data.Repositories;
using BookReviews.Data.Services;
using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;
using BookReviews.ThirdParty.GoogleBooks;
using BookReviews.ThirdParty.GoodReads;
using BookReviews.ThirdParty.GoodReads.Models;
using BookReviews.ThirdParty.Twitter;

using MongoDB.Bson;
using MongoDB.Driver;

namespace BookReviews.Import
{
    public class ImportDatabase
    {
        //private const string _connectionString = "mongodb://localhost/?safe=true";
        private const string _connectionString = "mongodb://admin:R34dIngIsFun!@ds039768.mongolab.com:39768/newbookreviews";
        private const string _databaseName = "newbookreviews";//"BookReviews";
        private MongoServer _server;
        private MongoDatabase _db;

        public ImportDatabase(bool createDatabase, bool importData, bool addGoogleData, bool addGoodReads, bool migrate)
            : this(new MongoClient(_connectionString).GetServer(), createDatabase, importData, addGoogleData, addGoodReads, migrate)
        {
        }

        public ImportDatabase(MongoServer server, bool createDatabase, bool importData, bool addGoogleData, bool addGoodReads, bool migrate)
        {
            var svc = new BookReviewService();
            var filter = new ReviewFilter()
            {
                Genre = 1,
                ItemsPerPage = 5,
                PageIndex = 0
            };
            var results = svc.SelectReviewsByDate(filter);
            //var repo = new BookReviewRepository();
            ////var reviews = repo.All();
            ////var reviews2 = repo.ByYear(2000);
            ////var review = repo.ById("51c76624895c9cf00546d054");

            //var svc = new BookReviewService();
            //var data = svc.SelectReviewsByDate(new Data.ViewModels.ReviewFilter()
            //{
            //    Genre = 0,
            //    ItemsPerPage = 5,
            //    PageIndex = 20
            //});


            //var reviews1 = repo.SelectReviewsByDate(20, 5, 0);
            //var reviews2 = repo.SelectReviewsByDate(20, 5, 1);

            var search = new TwitterSearch();
            var screenNames = ConfigurationManager.AppSettings["TwitterFeedScreenNames"]
                .Split(new char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            search.Connect(ConfigurationManager.AppSettings["TwitterConsumerKey"], ConfigurationManager.AppSettings["TwitterConsumerKey"]);

            var s = search.GetCurrentTweets(screenNames);

            _server = server;

            if (createDatabase)
            {
                if (_server.DatabaseExists(_databaseName))
                {
                    var result = _server.DropDatabase(_databaseName);
                }
            }

            _db = _server.GetDatabase(_databaseName);

            if (importData)
            {
                ImportData();
            }

            if (addGoogleData)
            {
                AddGoogleData();
            }

            if (addGoodReads)
            {
                AddGoodReads();
            }

            if (migrate)
            {
                MigrateData();
            }
        }

        private void MigrateData()
        {
            var newServer = MongoServer.Create(_connectionString);

            var existingDatabase = _server.GetDatabase(_databaseName);
            var newDatabase = newServer.GetDatabase("bookreviews");

            // GENRES
            var existingGenreCollection = existingDatabase.GetCollection("Genre");
            var newGenreCollection = newDatabase.GetCollection("Genre");

            var genres = existingGenreCollection.FindAllAs<Genre>().ToList();

            if (newGenreCollection.FindAllAs<Genre>().Count() == 0)
            {
                foreach (var genre in genres)
                {
                    newGenreCollection.Save(genre);
                }
            }

            // AUDIO PUBLISHERS
            var existingPubCollection = existingDatabase.GetCollection("AudioPublisher");
            var newPubCollection = newDatabase.GetCollection("AudioPublisher");

            var pubs = existingPubCollection.FindAllAs<AudioPublisher>().ToList();

            if (newPubCollection.FindAllAs<Genre>().Count() == 0)
            {
                foreach (var pub in pubs)
                {
                    newPubCollection.Save(pub);
                }
            }

            // AUTHORS
            var existingAuthorCollection = existingDatabase.GetCollection("Author");
            var newAuthorCollection = newDatabase.GetCollection("Author");

            var authors = existingAuthorCollection.FindAllAs<Author>().ToList();

            if (newAuthorCollection.FindAllAs<Author>().Count() == 0)
            {
                foreach (var author in authors)
                {
                    newAuthorCollection.Save(author);
                }
            }

            // REVIEWS
            var existingReviewCollection = existingDatabase.GetCollection("Review");
            var newReviewCollection = newDatabase.GetCollection("Review");

            var reviews = existingReviewCollection.FindAllAs<Review>().ToList();

            if (newReviewCollection.FindAllAs<Review>().Count() == 0)
            {
                foreach (var review in reviews)
                {
                    newReviewCollection.Save(review);
                }
            }
        }

        private void ImportData()
        {
            using (var conn = new OdbcConnection())
            {
                conn.ConnectionString = ConfigurationManager.AppSettings["AccessDatabaseConnectionString"];
                conn.Open();

                //var tables = conn.GetSchema("Views");

                ImportReviews(conn);
                ImportAudioPublishers(conn);
                ImportGenres(conn);
                ImportAuthors(conn);

                conn.Close();
            }
        }

        private void AddGoodReads()
        {
            var authorCollection = _db.GetCollection("Author");
            var reviewCollection = _db.GetCollection("Review");

            var authorRepository = new AuthorRepository();
            var reviewRepository = new BookReviewRepository();
            
            var search = new GoodReadsSearchApi();

            // BY ISBN
            var reviews = reviewRepository.AllReviews
                .Where(x => x.Google != null)
                .Where(x => x.GoodReads == null && x.GoodReadsAccessDate == null);

            var count = 1;
            var reviewCount = reviews.Count();

            foreach (var review in reviews)
            {
                foreach (var id in review.Google.IndustryIdentifiers)
                {
                    var data = search.FindGoodReadsDataByIsbn(id.identifier);

                    if (data != null)
                    {
                        if (review.GoodReads == null)
                        {
                            review.GoodReads = new GoodReadsBook()
                            {
                                Authors = data.Authors.Select(x => new GoodReadsSimpleAuthor()
                                {
                                    AuthorId = x.AuthorId,
                                    Name = x.Name
                                }).ToList(),
                                AverageRating = data.AverageRating,
                                GoodReadsBookId = data.BookId,
                                Url = data.Url,
                                ImageUrl = data.ImageUrl,
                                SmallImageUrl = data.SmallImageUrl,
                                SimilarBooks = data.SimilarBooks.Select(CreateGoodReadsSimilarBook).ToList()
                            };
                        }

                        foreach (var author in review.GoodReads.Authors)
                        {
                            var name = author.Name.ToLower();
                            var matches = authorRepository.AllAuthors
                                .Where(x => x.GoodReadsAccessDate == null)
                                .Where(x => !string.IsNullOrEmpty(x.LastName) && name.Contains(x.LastName.ToLower()))
                                .Where(x => !string.IsNullOrEmpty(x.FirstName) && name.Contains(x.FirstName.ToLower()))
                                .ToList();

                            BookAuthorData authorData = null;

                            if (matches.Count == 1)
                            {
                                author.InternalAuthorId = matches[0].OriginalId;
                                authorData = search.FindGoodReadsDataByAuthorId(author.AuthorId.ToString());
                            }
                            else
                            {
                                var matchFound = false;
                                foreach (var match in matches)
                                {
                                    var authorReviews = reviewRepository.ByAuthor(match.OriginalId);

                                    foreach (var authorReview in authorReviews)
                                    {
                                        if (authorReview.BookTitle == data.BookTitle)
                                        {
                                            author.InternalAuthorId = match.OriginalId;
                                            authorData = search.FindGoodReadsDataByAuthorId(author.AuthorId.ToString());

                                            // do conditional author lookup here!
                                            matchFound = true;
                                            break;
                                        }
                                    }

                                    if (matchFound)
                                        break;
                                }
                            }

                            if (authorData != null)
                            {
                                var mongoAuthor = authorRepository.ById(author.InternalAuthorId.Value);
                                // go thru the works for matches!
                                var goodReadsAuthor = new GoodReadsAuthor()
                                {
                                    AuthorId = authorData.AuthorId,
                                    AverageRating = authorData.AverageRating,
                                    DateOfBirth = authorData.DateOfBirth,
                                    DateOfDeath = authorData.DateOfDeath,
                                    Gender = authorData.Gender,
                                    Hometown = authorData.Hometown,
                                    ImageUrl = authorData.ImageUrl,
                                    Link = authorData.Link,
                                    Name = authorData.Name,
                                    RatingsCount = authorData.RatingsCount,
                                    SmallImageUrl = authorData.SmallImageUrl,
                                    WorksCount = authorData.WorksCount
                                };

                                foreach (var work in authorData.Works)
                                {
                                    int? internalBookId = null;

                                    var matchingReviews = reviewRepository.ByAuthor(author.InternalAuthorId.Value);

                                    foreach (var matchingReview in matchingReviews) {
                                        if (matchingReview.BookTitle.ToLower() == work.BookTitle.ToLower())
                                        {
                                            internalBookId = matchingReview.OriginalId;
                                            break;
                                        }
                                    }

                                    goodReadsAuthor.Works.Add(new GoodReadsSimpleBook()
                                    {
                                        BookId = work.BookId,
                                        BookTitle = work.BookTitle,
                                        InternalBookId = internalBookId
                                    });
                                }

                                mongoAuthor.GoodReadsAccessDate = DateTime.Now;
                                mongoAuthor.GoodReads = goodReadsAuthor;
                                //SAVE
                                authorCollection.Save<Author>(mongoAuthor);
                            }
                        }

                        System.Console.WriteLine(count + " of " + reviewCount + " : " + review.BookTitle + " has been processed");
                        count++;
                        break;
                    }
                }

                review.GoodReadsAccessDate = DateTime.Now;
                // write
                reviewCollection.Save<Review>(review);
                
            }
        }

        private GoodReadsBook CreateGoodReadsSimilarBook(BookData bd)
        {
            return new GoodReadsBook()
            {
                Authors = bd.Authors.Select(x => new GoodReadsSimpleAuthor()
                {
                    AuthorId = x.AuthorId,
                    Name = x.Name
                }).ToList(),
                GoodReadsBookId = bd.BookId,
                AverageRating = bd.AverageRating,
                Url = bd.Url,
                BookTitle = bd.BookTitle
            };
        }

        private void AddGoogleData()
        {
            var collection = _db.GetCollection("Review");
            var reviewRepository = new BookReviewRepository();
            var authorRepository = new AuthorRepository();

            var reviews = reviewRepository.AllReviews;// ByYear(2003);

            var search = new GoogleSearchApi();
            var reviewsForData = reviews.Where(x => x.Google == null);

            foreach (var book in reviewsForData)
            {
                foreach (var author in book.Authors.Where(x => x > 0))
                {
                    var authorInfo = authorRepository.ById(author);

                    if (authorInfo != null)
                    {
                        var bookCollection = search.FindMatchingBooks(authorInfo.FirstName + " " + authorInfo.LastName, book.BookTitle);

                        if (bookCollection.items.Count > 0)
                        {
                            book.Google = new GoogleBooksData();

                            var volume = bookCollection.items
                                .Where(x => x.volumeInfo.imageLinks.Count > 0)
                                .FirstOrDefault();

                            if (volume != null)
                            {
                                book.Google.ImageLinks = volume.volumeInfo.imageLinks;
                                book.Google.Categories = volume.volumeInfo.categories;
                                book.Google.IndustryIdentifiers = volume.volumeInfo.industryIdentifiers;
                            }
                            else
                            {
                                volume = bookCollection.items.First();
                                book.Google.Categories = volume.volumeInfo.categories;
                                book.Google.IndustryIdentifiers = volume.volumeInfo.industryIdentifiers;
                            }

                            var averageRatingAggregate = 0d;
                            var ratingsCount = 0;

                            foreach (var vol in bookCollection.items)
                            {
                                ratingsCount += vol.volumeInfo.ratingsCount;
                                averageRatingAggregate += vol.volumeInfo.ratingsCount * vol.volumeInfo.averageRating;
                            }

                            book.Google.AverageRating = Math.Round(averageRatingAggregate / ratingsCount, 1);
                            book.Google.RatingsCount = ratingsCount;

                            collection.Save<Review>(book);
                        }
                    }

                    break;
                }
            }
        }

        private void ImportReviews(OdbcConnection conn)
        {
            var collection = _db.GetCollection("Review");

            using (var cmd = new OdbcCommand("SELECT * FROM [Review Table]", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var review = new Review()
                    {
                        OriginalId = reader.GetInt32(reader.GetOrdinal("Key")),
                        BookTitle = reader.StringValue(reader.GetOrdinal("Book Title")),
                        Authors = new List<int>(),
                        PublicationDate = new DateTime?().ParseDate(reader.StringValue(reader.GetOrdinal("Pub Year")).ToInt(-1),
                            reader.StringValue(reader.GetOrdinal("Pub Month")).ToInt(1),
                            1),
                        IsAudio = reader.BoolValue(reader.GetOrdinal("Audio Book")),
                        Unabridged = reader.BoolValue(reader.GetOrdinal("Unabridged")),
                        NumberCassettes = reader.Int16Value(reader.GetOrdinal("Number of Cassettes")),
                        Narrators = new List<string>(),
                        NarrationRating = reader.StringValue(reader.GetOrdinal("How")),
                        Genre = reader.Int16Value(reader.GetOrdinal("Genre")),
                        Synopsis = reader.StringValue(reader.GetOrdinal("Synopsis")),
                        PageCount = reader.Int16Value(reader.GetOrdinal("Number of Pages")),
                        ContentNotes = reader.StringValue(reader.GetOrdinal("Content Notes")),
                        ShortReview = reader.StringValue(reader.GetOrdinal("Brief Review")),
                        LongReview = reader.StringValue(reader.GetOrdinal("Review")),
                        YearProduced = reader.StringValue(reader.GetOrdinal("Year Produced")).ToInt(-1),
                        PublicationNotes = reader.StringValue(reader.GetOrdinal("Publication Notes")),
                        Publisher = new PrintPublisher()
                        {
                            Name = reader.StringValue(reader.GetOrdinal("Publisher")),
                            Division = reader.StringValue(reader.GetOrdinal("Publisher Division")),
                            Location = reader.StringValue(reader.GetOrdinal("City and State"))
                        },
                        AudioPublisher = reader.Int16Value(reader.GetOrdinal("Audio Publisher")),
                        ReadDate = new DateTime?().ParseDate(reader.StringValue(reader.GetOrdinal("Yr Read"), "-1").ToInt(-1),
                            reader.StringValue(reader.GetOrdinal("Mo Read")).ToMonthNumber(),
                            1)
                    };

                    var narrator1 = reader.StringValue(reader.GetOrdinal("Narrator1"));
                    if (!string.IsNullOrEmpty(narrator1))
                    {
                        review.Narrators.Add(narrator1.ToString());
                    }

                    var narrator2 = reader.StringValue(reader.GetOrdinal("Narrator2"));
                    if (!string.IsNullOrEmpty(narrator2))
                    {
                        review.Narrators.Add(narrator2.ToString());
                    }

                    var narrator3 = reader.StringValue(reader.GetOrdinal("Narrator3"));
                    if (!string.IsNullOrEmpty(narrator3))
                    {
                        review.Narrators.Add(narrator3.ToString());
                    }

                    var author = reader.Int16Value(reader.GetOrdinal("Author"));
                    if (author != null && author > 0)
                    {
                        review.Authors.Add(author.Value);
                    }

                    var coauthor = reader.Int16Value(reader.GetOrdinal("Co-Author"));
                    if (coauthor != null && coauthor > 0)
                    {
                        review.Authors.Add(coauthor.Value);
                    }

                    collection.Insert(review);
                }

                reader.Close();
            }
        }

        private void ImportAudioPublishers(OdbcConnection conn)
        {
            var collection = _db.GetCollection("AudioPublisher");

            using (var cmd = new OdbcCommand("SELECT * FROM [AudioPub]", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var audioPublisher = new AudioPublisher()
                    {
                        OriginalId = reader.GetInt32(reader.GetOrdinal("Key")),
                        Name = reader.StringValue(reader.GetOrdinal("Audio Publisher"))
                    };

                    collection.Insert(audioPublisher);
                }

                reader.Close();
            }
        }

        private void ImportGenres(OdbcConnection conn)
        {
            var collection = _db.GetCollection("Genre");

            using (var cmd = new OdbcCommand("SELECT * FROM [Genre Table]", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var genre = new Genre()
                    {
                        OriginalId = reader.GetInt32(reader.GetOrdinal("Key")),
                        Name = reader.StringValue(reader.GetOrdinal("Genre"))
                    };

                    collection.Insert(genre);
                }

                reader.Close();
            }
        }

        private void ImportAuthors(OdbcConnection conn)
        {
            var collection = _db.GetCollection("Author");
            
            using (var cmd = new OdbcCommand("SELECT * FROM [Author Table]", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var author = new Author()
                    {
                        OriginalId = reader.GetInt32(reader.GetOrdinal("Key")),
                        LastName = reader.StringValue(reader.GetOrdinal("Last Name")),
                        FirstName = reader.StringValue(reader.GetOrdinal("First Name")),
                        Notes = reader.StringValue(reader.GetOrdinal("Notes"))
                    };

                    collection.Insert(author);
                }

                reader.Close();
            }
        }
    }
}



