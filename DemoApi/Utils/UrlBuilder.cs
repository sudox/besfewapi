using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Utils
{
    /*
     * This code is a slightly adapted version of this: https://blog.ploeh.dk/2020/08/10/an-aspnet-core-url-builder/
     * By the great Mark Seeman.
     * 
     */


    public class UrlBuilder
    {
        private readonly string action;
        private readonly string controller;
        private readonly object values;

        public UrlBuilder()
        {
        }

        private UrlBuilder(string action, string controller, object values)
        {
            this.action = action;
            this.controller = controller;
            this.values = values;
        }

        public UrlBuilder WithAction(string newAction)
        {
            return new UrlBuilder(newAction, controller, values);
        }

        public UrlBuilder WithValues(object newValues)
        {
            return new UrlBuilder(action, controller, newValues);
        }

        public UrlBuilder WithController(string newController)
        {
            if (newController is null)
                throw new ArgumentNullException(nameof(newController));

            const string controllerSuffix = "controller";

            var index = newController.LastIndexOf(
                controllerSuffix,
                StringComparison.OrdinalIgnoreCase);
            if (0 <= index)
                newController = newController.Remove(index);
            return new UrlBuilder(action, newController, values);
        }

        public Uri BuildAbsolute(LinkGenerator generator, HttpContext context)
        {

            var actionUrl = generator.GetUriByAction(
                context,
                action,
                controller,
                values
                );
            return new Uri(actionUrl);
        }
    }


    public record Link(string Rel, string Href);

    public static class Extensions
    {
        public static Link Link(this Uri uri, string rel)
        {
            return new Link( rel,  uri.ToString());
        }
    }
}
