using CacheCow.Server.Core.Mvc;
using DemoApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Controllers
{
    [Route("authors")]
    public class AuthorsController : ControllerBase
    {

        private readonly BooksDataContext _context;

        public AuthorsController(BooksDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GetAuthorsResponse>> GetAllAuthors()
        {
            var data = await _context.Authors
                .Select(a => new GetAuthorDetailsResponse(a.Id, a.FirstName, a.LastName))
                .ToListAsync();

            return Ok(new GetAuthorsResponse(data));
        }

        [HttpGet("{id:int}", Name ="get-author-by-id")]
        [HttpCacheFactory(3)]
        public async Task<ActionResult<GetAuthorDetailsResponse>> GetAuthorById(int id)
        {
            var response = await _context.Authors.Where(a => a.Id == id).Select(a => new GetAuthorDetailsResponse(a.Id, a.FirstName, a.LastName)).SingleOrDefaultAsync();

            if(response == null)
            {
                return NotFound();
            } else
            {
                return Ok(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetAuthorDetailsResponse>> AddAnAuthor([FromBody] PostAuthorRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } else
            {
                var author = new Author { FirstName = request.FirstName, LastName = request.LastName };
                try
                {
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
                    var response = new GetAuthorDetailsResponse(author.Id, author.FirstName, author.LastName);
                    return CreatedAtRoute("get-author-by-id", new { Id = author.Id }, response);
                }
                catch (DbUpdateException) // That author already exists!
                {
                    // options here: You could return OK or Accepted. I mean, you did what the person wanted.
                    // Or you could return a 303 - see other. we'll do that because it is more fun.
                    var id = await _context.Authors.Where(a => a.FirstName == request.FirstName && a.LastName == request.LastName)
                        .Select(a => a.Id).SingleOrDefaultAsync();
                   
                  
                    Response.Headers.Add("Location", Url.RouteUrl("get-author-by-id", new { Id = id }));
                    return StatusCode(303);
                }
            }

           
        }
        [HttpDelete("{id:int}")]
        [HttpCacheFactory(3)]
        public async Task<ActionResult> RemoveAuthor(int id)
        {
            var author = await _context.Authors.SingleOrDefaultAsync(a => a.Id == id);
            if(author != null)
            {
                try
                {
                    _context.Authors.Remove(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    var message = $"Cannot remove author {id} because they have associated books";

                    return Conflict(new { message });
                }
            }

            return NoContent(); // groovy

        }
        
        [HttpPut("{id:int}")]
        [HttpCacheFactory()]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] GetAuthorDetailsResponse request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } else
            {
                var savedAuthor = await _context.Authors.SingleOrDefaultAsync(a => a.Id == id);
                if(savedAuthor == null)
                {
                    return NotFound();
                } else
                {
                    savedAuthor.FirstName = request.FirstName;
                    savedAuthor.LastName = request.LastName;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }

            }
        }
    }

    public record GetAuthorDetailsResponse(int Id, string FirstName, string LastName);
    public record GetAuthorsResponse(IList<GetAuthorDetailsResponse> Data);
    public record PostAuthorRequest([Required] string FirstName, [Required] string LastName);
}
