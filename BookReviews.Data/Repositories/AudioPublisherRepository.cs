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
    public class AudioPublisherRepository : RepositoryBase, IAudioPublisherRepository
    {
        private MongoCollection AudioPublishers { get; set; }

        public AudioPublisherRepository()
            : base()
        {
            AudioPublishers = Database.GetCollection<BsonDocument>("AudioPublisher");
        }

        public List<AudioPublisher> All()
        {
            var results = AudioPublishers.FindAllAs<AudioPublisher>();

            //switch (orderByKey)
            //{
            //    case "READDATE":
            //        results.OrderBy(x => x.ReadDate);
            //        break;
            //    case "READDATE":
            //        results.OrderBy(x => x.ReadDate);
            //        break;
            //}
            
            return results.ToList();
        }


        // CRUD OPERATIONS
    }
}