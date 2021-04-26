using DemoApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Models
{
   public record GetAuthorDetailsResponse(
       string FirstName,
       string LastName
      ): GetResponse();
}
