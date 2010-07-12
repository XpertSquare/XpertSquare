using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XpertSquare.Web.Helpers
{
    public static class AppContentHelper
    {
        /// <summary>
        /// Builds an Image URL
        /// </summary>
        /// <param name="imageFile">The file name of the image</param>
        public static string ImageUrl(string imageFile)
        {
            return VirtualPathUtility.ToAbsolute("~/content/images/" + imageFile);
        }

        /// <summary>
        /// Builds a CSS URL
        /// </summary>
        /// <param name="cssFile">The name of the CSS file</param>
        public static string CssUrl(string cssFile)
        {
            return VirtualPathUtility.ToAbsolute("~/content/" + cssFile);
        }

        /// <summary>
        /// Builds a Jquery JS URL
        /// </summary>
        /// <param name="cssFile">The name of the CSS file</param>
        public static string JavaScriptUrl(string javaScriptUrl)
        {
            return VirtualPathUtility.ToAbsolute("~/content/js/" + javaScriptUrl);
        }
    }
}
