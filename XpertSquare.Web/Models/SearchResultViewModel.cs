using System;
using System.Collections.Generic;
using XpertSquare.Core.Model;
using XpertSquare.Core;

namespace XpertSquare.Web.Models
{
    public class SearchResultViewModel
    {
        public IPagination Questions { get; set; }
        public String SearchQuery { get; set; }
        public String SearchSuggestion { get; set; }
    }
}
