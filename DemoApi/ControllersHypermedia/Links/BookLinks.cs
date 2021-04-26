using DemoApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Links
{
    public class BookLinks
    {
        private readonly LinkGenerator _generator;
        private readonly HttpContext _context;

        private readonly static UrlBuilder booksControllerLink = new UrlBuilder()
            .WithController(nameof(BooksController));


        public BookLinks(LinkGenerator generator, IHttpContextAccessor context)
        {
            _generator = generator;
            _context = context.HttpContext;
        }



        

        public IEnumerable<Link> GetBooksLinks()
        {
            yield return booksControllerLink
            .WithAction(nameof(BooksController.Get))
                .BuildAbsolute(_generator, _context).Link(LinkRelations.Self);

            var link = new UrlBuilder()
               .WithAction(nameof(AuthorsController.Get))
               .WithController(nameof(AuthorsController))
               .WithValues(new { id = 999 })
               .BuildAbsolute(_generator, _context).Link(LinkRelations.AuthorDetails);

            yield return link with { Href = link.Href.Replace("999", "{id}") };


        }

        public IEnumerable<Link> GetBookDetailsLinks(string bookId, int authorId)
        {

            yield return booksControllerLink
                .WithAction(nameof(BooksController.GetDetails))
                .WithValues(new { id = bookId })
                .BuildAbsolute(_generator, _context).Link(LinkRelations.Self);

            yield return new UrlBuilder()
                .WithController(nameof(AuthorsController))
                .WithAction(nameof(AuthorsController.GetById))
                .WithValues(new { id = authorId })
                .BuildAbsolute(_generator, _context)
                .Link(LinkRelations.AuthorDetails);

        }
    }
}
