using DemoApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Links
{
    public class AuthorLinks
    {

        private readonly LinkGenerator _generator;
        private readonly HttpContext _context;

        public AuthorLinks(LinkGenerator generator, IHttpContextAccessor context)
        {
            _generator = generator;
            _context = context.HttpContext;
        }

        private readonly static UrlBuilder authorsControllerLink = new UrlBuilder()
            .WithController(nameof(AuthorsController));


        public IList<Link> GetLinks()
        {
            return new List<Link>();
        }

        public Link GetAuthorDetailsLink(int id)
        {
            return authorsControllerLink
                .WithAction(nameof(AuthorsController.GetById))
                .WithValues(new { id = id })
                .BuildAbsolute(_generator, _context)
                .Link(LinkRelations.Self);
        }
    }
}
