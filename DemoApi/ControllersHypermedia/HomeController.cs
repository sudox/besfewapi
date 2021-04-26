using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApi.Utils;
using DemoApi.LinkCollections;
using Microsoft.AspNetCore.Routing;

namespace DemoApi.Controllers
{
    [Route("/")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 20)]
    public class HomeController : ControllerBase
    {
        private readonly LinkGenerator _generator;

        public HomeController(LinkGenerator generator)
        {
            _generator = generator;
        }

        [HttpGet]
        public ActionResult GetHomeDoc()
        {
            var homeLinks = new HomeLinks();
            var links = homeLinks.GetLinks(HttpContext,_generator);
            var response = new
            {
                _links = links
            };

            return Ok(response);

        }
    }
}
