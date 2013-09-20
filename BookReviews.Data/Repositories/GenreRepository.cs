using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookReviews.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace BookReviews.Data.Repositories
{
    public class GenreRepository : RepositoryBase, IGenreRepository
    {
        private MongoCollection Genres { get; set; }

        public GenreRepository()
            : base()
        {
            Genres = Database.GetCollection<BsonDocument>("Genre");
        }

        public List<Genre> All()
        {
            var results = Genres.AsQueryable<Genre>()
                .OrderBy(x => x.Name)
                .Select(x => x.Serializable());

            return results.ToList();
        }

        // CRUD OPERATIONS
    }
}