using DemoApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Links
{
    public class HomeLinks
    {

        private readonly LinkGenerator _generator;
        private readonly HttpContext _context;
        public HomeLinks(IHttpContextAccessor contextAccessor, LinkGenerator generator)
        {
            _context = contextAccessor.HttpContext;
            _generator = generator;
        }

        private readonly static UrlBuilder home =
    new UrlBuilder()
        .WithAction(nameof(StoreController.Get))
        .WithController(nameof(StoreController));

   
   

        public IList<Link> GetLinks()
        {
            var links = new List<Link>();
      
            var store = new UrlBuilder()
                .WithAction(nameof(StoreController.Get))
                .WithController(nameof(StoreController));

            var books = new UrlBuilder()
                .WithAction(nameof(BooksController.Get))
                .WithController(nameof(BooksController));

            var authors = new UrlBuilder()
                .WithAction(nameof(AuthorsController.Get))
                .WithController(nameof(AuthorsController));

            links.Add(store.BuildAbsolute(_generator, _context).Link(LinkRelations.Self));
            links.Add(books.BuildAbsolute(_generator, _context).Link(LinkRelations.Books));
            links.Add(authors.BuildAbsolute(_generator, _context).Link(LinkRelations.Authors));
         
      

            return links;
        }
    }
}
