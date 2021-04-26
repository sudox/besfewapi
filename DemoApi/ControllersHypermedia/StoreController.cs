using DemoApi.ControllersHypermedia.Models;
using DemoApi.ControllersHypermedia.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia
{
    [Route("store")]
    public class StoreController : ControllerBase
    {
        private readonly IHomeService _service;

        public StoreController(IHomeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
        public async Task<ActionResult<StoreGetResponse>> Get()
        {
            StoreGetResponse response = await _service.GetAsync();
            return Ok(response);
        }
    }

   
}
