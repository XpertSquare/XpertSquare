using System;
using System.Collections.Generic;

using XpertSquare.Core.Model;
using XpertSquare.Core;


namespace XpertSquare.Web.Models
{
    public class TaggedViewModel
    {
        public Int32 CurrentPageSize { get; set; }
        public IPagination Questions { get; set; }
        public String TagName { get; set; }
    }
}
