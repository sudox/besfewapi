using DemoApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Models
{
    public record StoreGetResponse(
        int NumberOfAuthors,
        int NumberOfBooks,
        [property: JsonPropertyName("_links")]
        IList<Link> Links);
}
