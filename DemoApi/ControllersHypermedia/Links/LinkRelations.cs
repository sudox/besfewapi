using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Links
{

    public record LinkRelations
    {
        // https://www.iana.org/assignments/link-relations/link-relations.xhtml
        /// <summary>
        /// Adds the "self" link relation
        /// </summary>
        public static string Self = "self";
        /// <summary>
        /// Adds the "next" link relation (for paginated results)
        /// </summary>
        public static string Next = "next";
        /// <summary>
        /// Adds the "previous" link relation (for paginated results)
        /// </summary>
        public static string Prev = "previous";
        /// <summary>
        /// Adds the "current" linkk relation (for paginated results)
        /// </summary>
        public static string Current = "current";

        // for this API - "ht" is an arbitrary prefix denoting "HyperTheory"

        /// <summary>
        /// Adds the "ht:books" link relation (a link to the books)
        /// </summary>
        public static string Books = "ht:books";
        /// <summary>
        /// Adds the ht:book-details link relation (a link to the book details)
        /// </summary>
        public static string BookDetails = "ht:book-details";

        /// <summary>
        /// Adds the "ht:authors" link relation (a link the authors)
        /// </summary>
        public static string Authors = "ht:authors";
        /// <summary>
        /// Adds the "ht:author-details" link relation (a link to the author details)
        /// </summary>
        public static string AuthorDetails = "ht:author-details";

    }


}
