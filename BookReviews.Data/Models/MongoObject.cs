using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace BookReviews.Data.Models
{
    public class MongoObject
    {
        public BsonObjectId _id { get; set; }
    }
}

