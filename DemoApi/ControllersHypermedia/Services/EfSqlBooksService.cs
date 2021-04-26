using DemoApi.ControllersHypermedia.Links;
using DemoApi.ControllersHypermedia.Models;
using DemoApi.Domain;
using DemoApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Services
{
    public class EfSqlBooksService : IBooksService
    {
        private readonly BooksDataContext _dbContext;
        private readonly BookLinks _bookLinks;
        private readonly AuthorLinks _authorLinks;

        public EfSqlBooksService(BooksDataContext dbContext, BookLinks bookLinks, AuthorLinks authorLinks)
        {
            _dbContext = dbContext;
            _bookLinks = bookLinks;
            _authorLinks = authorLinks;
        }

        public async Task<GetBooksResponse> GetAsync()
        {
   

            var data = await _dbContext.Books
                .Include(b => b.Author).ToListAsync();


            var embeddedBooks = new List<GetBookDetailsResponse>();

            foreach (var book in data)
            {
                var authorLinks = new List<Link>();
                authorLinks.Add(_authorLinks.GetAuthorDetailsLink(book.Author.Id));
                authorLinks.AddRange(_authorLinks.GetLinks());

                var author = new GetAuthorDetailsResponse(book.Author.FirstName, book.Author.LastName) { Links = authorLinks };

           
                embeddedBooks.Add(new GetBookDetailsResponse(book.ISBN, book.Title)
                {
                    Embedded = new List<GetAuthorDetailsResponse>() { author },
                    Links = _bookLinks.GetBookDetailsLinks(book.ISBN, book.Author.Id).ToList()
                }) ;

            }

            return new GetBooksResponse() { Embedded = embeddedBooks, Links = _bookLinks.GetBooksLinks().ToList()};
        }

        private UrlBuilder GetAuthorDetailsLink()
        {
            return new UrlBuilder()
               .WithAction(nameof(AuthorsController.GetById))
               .WithController(nameof(AuthorsController));
        }

 
    }
}
