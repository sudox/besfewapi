using CacheCow.Server.Core.Mvc;
using DemoApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Controllers
{
    [Route("v1/books")]
    public class BooksV1Controller :ControllerBase
    {
        private readonly BooksDataContext _context;

        public BooksV1Controller(BooksDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[ResponseCache(Location =ResponseCacheLocation.Any, Duration = 30)]
        //[HttpCacheFactory(90)]
        public async Task<ActionResult<GetBooksResponse>> GetAllBooks()
        {
            var books = await _context.
               Books
               .Select(b=> new BooksModel(b.ISBN, b.Title, b.Author.FirstName + ' ' + b.Author.LastName))
               .ToListAsync();
            return Ok(new GetBooksResponse(books));
        }
    }

    public record BooksModel(string ISBN, string Title, string Author);
    public record GetBooksResponse(IList<BooksModel> Data);
}
