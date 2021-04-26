using DemoApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Models
{
    public record GetBooksResponse() :
        GetTypedEmbeddedResponse<GetBookDetailsResponse>();


    public record GetBookDetailsResponse(string Isbn,string Title): GetTypedEmbeddedResponse<GetAuthorDetailsResponse>();
}
