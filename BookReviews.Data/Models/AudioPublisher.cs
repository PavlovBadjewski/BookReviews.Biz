using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace BookReviews.Data.Models
{
    public class AudioPublisher : AbstractPublisher
    {
        public int OriginalId { get; set; }                 // [key]
        public string Name { get; set; }            // [Audio Publisher]
        /*
	DECLARE @audioId AS int
	DECLARE @audioNotes as varchar(8000)
         */
    }
}
