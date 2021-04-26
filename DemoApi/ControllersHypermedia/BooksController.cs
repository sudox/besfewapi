using DemoApi.ControllersHypermedia.Models;
using DemoApi.ControllersHypermedia.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia
{
    [Route("store/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public ActionResult GetDetails(string id)
        {
            return Ok();
        }
        [HttpGet("")]
        public async Task< ActionResult> Get()
        {
            GetBooksResponse response = await _service.GetAsync();
            return Ok(response);
        }

     
    }
}
