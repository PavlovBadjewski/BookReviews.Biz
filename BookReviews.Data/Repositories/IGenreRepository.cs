using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BookReviews.Data.Models;

namespace BookReviews.Data.Repositories
{
    public interface IGenreRepository
    {
        List<Genre> All();
    }
}
