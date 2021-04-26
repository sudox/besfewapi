using DemoApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Models
{

    public abstract record GetResponse()
    {
        [JsonPropertyName("_links")]
        public IList<Link> Links { get; init; }

        [JsonPropertyName("_embedded")]
        public IList<object> Embedded { get; init; }

    };
    public abstract record GetTypedEmbeddedResponse<T>()
    {
        [JsonPropertyName("_links")]
        public IList<Link> Links { get; init; }

        [JsonPropertyName("_embedded")]
        public IList<T> Embedded { get; init; }
    }

}
