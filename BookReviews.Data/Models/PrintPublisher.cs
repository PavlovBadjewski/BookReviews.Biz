using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Data.Models
{
    public class PrintPublisher : AbstractPublisher
    {
        public string Name { get; set; }
        public string Division { get; set; }
        public string Location { get; set; }
        /*
        DECLARE @printId AS int
	    DECLARE @printPublisher as varchar(60)
	    DECLARE @printDivision as varchar(60)
	    DECLARE @printLocation as varchar(30)
	    DECLARE @currentId AS int
	    DECLARE @currentPublisher as varchar(60)
	    DECLARE @currentDivision as varchar(60)
	    DECLARE @currentLocation as varchar(30)
        */
    }
}
