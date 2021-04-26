using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia
{
    [Route("store/authors")]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            return Ok();
        }
    }
}
