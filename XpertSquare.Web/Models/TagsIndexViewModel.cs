using System;

using XpertSquare.Core;

namespace XpertSquare.Web.Models
{
    public class TagsIndexViewModel
    {
        public Int32 CurrentPageSize { get; set; }
        public IPagination Tags { get; set; }
    }
}
