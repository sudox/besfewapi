using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApi.Controllers;
using DemoApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DemoApi.LinkCollections
{
    public class HomeLinks
    {
        private readonly static UrlBuilder home =
            new UrlBuilder()
                .WithAction(nameof(HomeController.GetHomeDoc))
                .WithController(nameof(HomeController));

        private readonly static UrlBuilder booksV1 =
        new UrlBuilder()
            .WithAction(nameof(BooksV1Controller.GetAllBooks))
            .WithController(nameof(BooksV1Controller));

        private readonly static UrlBuilder authors =
        new UrlBuilder()
            .WithAction(nameof(AuthorsController.GetAllAuthors))
            .WithController(nameof(AuthorsController));

        public IList<Link> GetLinks(HttpContext context,LinkGenerator url)
        {
            var links = new List<Link>();

            links.Add(home.BuildAbsolute(url,context).Link("_self"));
            links.Add(booksV1.BuildAbsolute(url, context).Link("ht:booksv1"));
            links.Add(authors.BuildAbsolute(url, context).Link("ht:authors"));

            return links;
        }
    }
}
