using DemoApi.ControllersHypermedia.Links;
using DemoApi.ControllersHypermedia.Models;
using DemoApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Services
{
    public class EfSqlHomeService : IHomeService
    {
        private readonly BooksDataContext _dbContext;
        private readonly HomeLinks _homeLinks;

        public EfSqlHomeService(BooksDataContext dbContext, HomeLinks homeLinks)
        {
            _dbContext = dbContext;
            _homeLinks = homeLinks;
        }

        public async Task<StoreGetResponse> GetAsync()
        {
            var numberOfAuthors = await _dbContext.Authors.CountAsync();
            var numberOfBooks = await _dbContext.Books.CountAsync();
            var links = _homeLinks.GetLinks();
            return new StoreGetResponse(numberOfAuthors, numberOfBooks, links);
        }
    }
}
