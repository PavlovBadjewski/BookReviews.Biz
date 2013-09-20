using System;
using System.Collections.Generic;
using System.Linq;
using BookReviews.Data.Models;
using BookReviews.Data.ViewModels;

namespace BookReviews.Data.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> AllAuthors { get; set; }
        Author ById(string id);
        Author ById(int id);
        void AttachAuthorInformation(AuthorViewModel author);

        Author RandomAuthor();
        IEnumerable<Author> SelectAuthorsByDate(int pageIndex, int authorCount, AuthorFilter filter);
        AuthorViewModel BuildAuthorViewModel(Author a);
        int GetTotalAuthors(AuthorFilter filter);
    }
}
