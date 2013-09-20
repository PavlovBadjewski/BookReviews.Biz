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
using BookReviews.ThirdParty.Twitter;


namespace BookReviews.Data.Repositories
{
    public class AuthorRepository : RepositoryBase, IAuthorRepository
    {
        public List<Author> AllAuthors { get; set; }

        public AuthorRepository()
            : base()
        {
            if (Database != null)
            {
                AllAuthors = Database.GetCollection<BsonDocument>("Author").AsQueryable<Author>().ToList();
            }
        }

        public int GetTotalAuthors(AuthorFilter filter)
        {
            return (int)AllAuthors.Count();
        }

        public Author ById(int id)
        {
            return AllAuthors.SingleOrDefault(x => x.OriginalId == id);
        }

        public Author ById(string id)
        {
            return AllAuthors.SingleOrDefault(x => x._id.ToString() == id);
        }

        public void AttachAuthorInformation(AuthorViewModel author)
        {
            var data = ById(author.OriginalId);

            if (data != null)
            {
                author.Id = data._id.ToString();
                author.LastName = data.LastName;
                author.FirstName = data.FirstName;
            }
        }

        public Author RandomAuthor()
        {
            var authors = AllAuthors
                .Where(x => x.GoodReads != null).ToList();

            var position = (int)(DateTime.Now.Ticks % authors.Count());

            return authors[position];
        }

        public IEnumerable<Author> SelectAuthorsByDate(int pageIndex, int authorCount, AuthorFilter filter)
        {
            var authors = AllAuthors
                .Skip(pageIndex * authorCount)
                .Take(authorCount);

            return authors;
        }

        public AuthorViewModel BuildAuthorViewModel(Author a)
        {
            return new AuthorViewModel()
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Notes = a.Notes,
                Id = a._id.ToString(),
                GoodReads = a.GoodReads
            };
        }


        // CRUD OPERATIONS
    }
}