using System;
using System.Collections.Generic;

namespace XpertSquare.Web.Models
{
    public class AutocompleteResultModel
    {
        public String query { get; set; }
        public IList<String> suggestions { get; set; }
        public IList<String> data { get; set; }
    }
}
