using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace BookReviews.Data.Models
{
    public class Genre : MongoObject
    {
        public int OriginalId { get; set; }                 // [Key]
        public string Name { get; set; }            // [Genre]
    }
}
